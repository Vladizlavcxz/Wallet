using Wallet.Core.Models;

namespace Wallet.Api.Models
{
    public class OperationModel
    {
        public long UserId { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
    }

    public class ConvertationModel
    {
        public long UserId { get; set; }
        public Currency SourceCurrency { get; set; }
        public Currency RecipientCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
