using System;
using Wallet.Core.Models;
using Wallet.Data.Contracts;
using Wallet.Services.Contracts;

namespace Wallet.Services.Implementations
{
    public class AccessControlService : IAccessControlService
    {
        private readonly IUserRepository _userRepository;

        public AccessControlService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Registration.Models.OutModel Registration(Registration.Models.InModel inModel)
        {
            if (IsUserExist(inModel.Name, out User existingUser))
            {
                throw new InvalidOperationException($"User with name {existingUser.Name} already exists");
            }

            var user = new User()
            {
                Name = inModel.Name
            };

            _userRepository.Create(user);
            return new Registration.Models.OutModel(user.Id);
        }

        private bool IsUserExist(string name, out User user)
        {
            var userFromRepository = _userRepository.GetByName(name);

            if (userFromRepository != null)
            {
                user = userFromRepository;
                return true;
            }

            user = null;
            return false;
        }
    }
}
