using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Wallet.Api.Helpers
{
    public static class ControllerValidationExtension
    {
        public static string GetValidationErrors(this ControllerBase controllerBase, ValidationResult validationResult)
        {
            var errors = new StringBuilder();
            foreach (var error in validationResult.Errors)
            {
                errors.Append(error);
            }

            return errors.ToString();
        }
    }
}
