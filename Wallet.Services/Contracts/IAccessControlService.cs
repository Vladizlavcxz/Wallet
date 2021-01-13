
namespace Wallet.Services.Contracts
{
    public interface IAccessControlService
    {
        Registration.Models.OutModel Registration(Registration.Models.InModel inModel);
    }
}
