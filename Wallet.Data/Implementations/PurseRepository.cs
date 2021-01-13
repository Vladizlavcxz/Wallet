using System.Collections.Generic;
using System.Linq;
using Wallet.Core.Models;
using Wallet.Data.Context.Contract;
using Wallet.Data.Contracts;

namespace Wallet.Data.Implementations
{
    public class PurseRepository : IPurseRepository
    {
        private readonly IDbContext _dbContext;

        public PurseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Purse item)
        {
            _dbContext.Purses.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var purse = _dbContext.Purses.Find(id);

            if (purse != null)
            {
                _dbContext.Purses.Remove(purse);
                _dbContext.SaveChanges();
            }
        }

        public Purse Get(long userId, Currency currency)
        {
            return _dbContext.Purses.Find(userId, currency);
        }

        public IEnumerable<Purse> GetAll()
        {
            return _dbContext.Purses.ToList();
        }

        public IEnumerable<Purse> GetUserPurses(long userId)
        {
            return _dbContext.Purses.Where(u => u.UserId == userId);
        }

        public void Update(Purse item)
        {
            var purse = _dbContext.Purses.Find(item.UserId, item.CurrencyType);

            if (purse != null)
            {
                purse = item;

                _dbContext.Purses.Update(purse);
                _dbContext.SaveChanges();
            }
        }
    }
}
