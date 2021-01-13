using System;
using System.Collections.Generic;
using Wallet.Core.Models;
using Wallet.Data.Contracts;
using Wallet.Services.Contracts;

namespace Wallet.Services.Implementations
{
    public class PurseService : IPurseService
    {
        private readonly IPurseRepository _purseRepository;
        private readonly ICurrencyService _currencyService;

        public PurseService(IPurseRepository purseRepository, ICurrencyService currencyService)
        {
            _purseRepository = purseRepository;
            _currencyService = currencyService;
        }

        public void Add(long userId, Currency currency, decimal amount)
        {
            var purse = GetPurse(userId, currency);
            purse.Amount += amount;

            _purseRepository.Update(purse);
        }

        public void Convert(long userId, Currency currencySource, Currency currencyTarget, decimal amount)
        {
            var source = GetPurse(userId, currencySource);
            var recipient = GetPurse(userId, currencyTarget);

            var rate = _currencyService.GetRate(currencySource, currencyTarget);

            if (source.Amount >= amount)
            {
                source.Amount -= amount;
                recipient.Amount += amount * rate;

                _purseRepository.Update(source);
                _purseRepository.Update(recipient);
            }
            else
            {
                throw new InvalidOperationException("The amount on the source purse is less than the one you are trying to convert.");
            }
        }

        public void Withdraw(long userId, Currency currency, decimal amount)
        {
            var purse = GetPurse(userId, currency);
            if (purse.Amount >= amount)
            {
                purse.Amount -= amount;

                _purseRepository.Update(purse);
            }
            else
            {
                throw new InvalidOperationException("The amount on the purse is less than the one you are trying to withdraw.");
            }
        }


        public IEnumerable<Purse> GetPurses(long userId)
        {
            return _purseRepository.GetUserPurses(userId);
        }

        private Purse GetPurse(long userId, Currency currency)
        {
            if (!IsPurseExist(userId, currency, out Purse purse))
            {
                CreatePurse(userId, currency);
                purse = _purseRepository.Get(userId, currency);
            }

            return purse;
        }

        private void CreatePurse(long userId, Currency currency)
        {
            _purseRepository.Create(new Purse
            {
                UserId = userId,
                CurrencyType = currency
            });
        }

        private bool IsPurseExist(long userId, Currency currency, out Purse purse)
        {
            var purseFromRepository = _purseRepository.Get(userId, currency);

            if (purseFromRepository != null)
            {
                purse = purseFromRepository;
                return true;
            }

            purse = null;
            return false;
        }
    }
}
