using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Core.Models;

namespace Wallet.Services.Contracts
{
    public interface IPurseService
    {
        void Add(long userId, Currency currency, decimal amount);
        void Withdraw(long userId, Currency currency, decimal amount);
        void Convert(long userId, Currency currencySource, Currency currencyTarget, decimal amount);
        IEnumerable<Purse> GetPurses(long userId);
    }
}
