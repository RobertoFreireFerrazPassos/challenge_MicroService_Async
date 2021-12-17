using ApiAppShop.Domain.Services;
using ApiUser.Application.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiAppShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));
        }

        [HttpGet("setapp")]
        public IActionResult Set(AppResponse appCreationRequest)
        {
            _appService.SetItem(appCreationRequest);
            return Ok();
        }

        [HttpGet("getapps")]
        public IActionResult Get()
        {
            return Ok(_appService.GetItems<AppResponse>());
        }

        [HttpPost("purchase")]
        public IActionResult Purchase([FromBody] PurchaseRequest purchaseRequest)
        {
            return Ok();
        }
    }
}
