using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Models;
using Wallet.Core.Models;
using Wallet.Services.Contracts;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurseController : ControllerBase
    {
        private readonly IPurseService _purseService;

        public PurseController(IPurseService purseService)
        {
            _purseService = purseService;
        }

        [HttpGet]
        [Route("All")]
        public IEnumerable<Purse> GetPurses([FromQuery] long userId)
        {
            return _purseService.GetPurses(userId);
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult Add([FromBody] OperationModel addModel)
        {
            _purseService.Add(addModel.UserId, addModel.Currency, addModel.Amount);
            return Ok();
        }

        [HttpPost]
        [Route("Withdraw")]
        public ActionResult Withdraw([FromBody] OperationModel operationModel)
        {
            _purseService.Withdraw(operationModel.UserId, operationModel.Currency, operationModel.Amount);
            return Ok();
        }

        [HttpPost]
        [Route("Convert")]
        public ActionResult Convert([FromBody] ConvertationModel convertationModel)
        {
            _purseService.Convert(convertationModel.UserId, convertationModel.SourceCurrency, convertationModel.RecipientCurrency, convertationModel.Amount);
            return Ok();
        }
    }
}