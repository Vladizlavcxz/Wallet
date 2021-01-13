using System.Collections.Generic;
using Wallet.Core.Models;

namespace Wallet.Data.Contracts
{
    public interface IPurseRepository
    {
        IEnumerable<Purse> GetAll();
        Purse Get(long userId, Currency currency);
        IEnumerable<Purse> GetUserPurses(long userId);
        void Create(Purse item);
        void Update(Purse item);
        void Delete(int id);
    }
}
