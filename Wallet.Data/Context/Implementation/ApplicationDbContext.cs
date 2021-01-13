using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.Core.Models;
using Wallet.Data.Context.Contract;

namespace Wallet.Data.Context.Implementation
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Purse> Purses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding a composite primary key to the Purse entity
            modelBuilder.Entity<Purse>().HasKey(u => new { u.UserId, u.CurrencyType });
        }

        public void EnsureCreated()
        {
            if (Database.EnsureCreated())
            {
                var usersInitList = new List<User>
                {
                    new User
                    {
                        Id = 1,
                        Name = "User1"
                    },
                    new User
                    {
                        Id = 2,
                        Name = "User2"
                    },
                    new User
                    {
                        Id = 3,
                        Name = "User3"
                    }
                };
                using (var transaction = Database.BeginTransaction())
                {
                    Users.AddRange(usersInitList);
                    SaveChanges();

                    transaction.Commit();
                }
            }
        }

        public void EnsureDeleted()
        {
            Database.EnsureDeleted();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
