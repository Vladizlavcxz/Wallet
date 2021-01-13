using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wallet.Core.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IEnumerable<Purse> Wallets { get; set; }
    }
}
