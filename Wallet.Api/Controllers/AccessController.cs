using Microsoft.AspNetCore.Mvc;
using System;
using Wallet.Api.Helpers;
using Wallet.Services.Contracts;

using Auth = Wallet.Services.Authentication.Models;
using Registration = Wallet.Services.Registration.Models;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessControlService _accessControlService;

        public AccessController(IAccessControlService accessControlService)
        {
            _accessControlService = accessControlService;
        }

        [HttpPost]
        [Route("Registration")]
        public Registration.OutModel Registration([FromBody] Registration.InModel inModel)
        {
            var validationResult = new Registration.Validator().Validate(inModel);

            if (!validationResult.IsValid)
            {
                throw new InvalidOperationException(this.GetValidationErrors(validationResult));
            }

            return _accessControlService.Registration(inModel);
        }
    }
}