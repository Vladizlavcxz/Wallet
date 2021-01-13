using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wallet.Core.Models
{
    public class Purse
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public Currency CurrencyType { get; set; }
        public decimal Amount { get; set; }
    }
}
