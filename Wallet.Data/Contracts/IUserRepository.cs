using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Core.Models;

namespace Wallet.Data.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User GetByName(string name);
        void Create(User item);
        void Update(User item);
        void Delete(int id);
    }
}
