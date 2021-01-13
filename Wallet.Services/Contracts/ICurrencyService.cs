
using Wallet.Core.Models;

namespace Wallet.Services.Contracts
{
    public interface ICurrencyService
    {
        decimal GetRate(Currency currencySource, Currency currencyRecipient);
    }
}
