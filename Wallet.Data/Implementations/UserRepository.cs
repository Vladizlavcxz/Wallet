using System;
using System.Collections.Generic;
using System.Linq;
using Wallet.Core.Models;
using Wallet.Data.Context.Contract;
using Wallet.Data.Contracts;

namespace Wallet.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(User item)
        {
            _dbContext.Users.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.Find(id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public User Get(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetByName(string name)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Name == name);
        }

        public void Update(User item)
        {
                var user = _dbContext.Users.Find(item.Id);

            if (user != null)
            {
                user = item;

                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
            }
        }
    }
}
