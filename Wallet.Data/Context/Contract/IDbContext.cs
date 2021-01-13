using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wallet.Core.Models;

namespace Wallet.Data.Context.Contract
{
    public interface IDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Purse> Purses { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void EnsureCreated();
        void EnsureDeleted();
    }
}
