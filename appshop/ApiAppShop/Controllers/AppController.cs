using ApiAppShop.Application.DataContracts.Requests.App;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Services;
using ApiUser.Application.DataContracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private IAppService _appService;
        private IPurchaseService _purchaseService;
        private readonly IMapper _mapper;

        public AppController(IAppService appService,
            IPurchaseService purchaseService,
            IMapper mapper)
        {
            _appService = appService ??
                throw new ArgumentNullException(nameof(appService));
            _purchaseService = purchaseService ??
                throw new ArgumentNullException(nameof(purchaseService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("setapp")]
        public IActionResult Set(AddAppRequest addAppRequest)
        {
            _appService.AddApp(_mapper.Map<AppDto>(addAppRequest));
            return Ok();
        }

        [HttpGet("getapps")]
        public IActionResult Get()
        {
            var apps = _appService.GetApps();
            var response = new AppResponse()
            {
                Apps = _mapper.Map<IEnumerable<App>>(apps)
            };
            return Ok(_mapper.Map<AppResponse>(response));
        }

        [HttpGet("getappsbyuser/{userid}")]
        public IActionResult GetByUser(string userid)
        {
            var apps = _appService.GetAppsByUser(userid);
            var response = new AppResponse()
            {
                Apps = _mapper.Map<IEnumerable<App>>(apps)
            };
            return Ok(response);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequest purchaseRequest)
        {
            try
            {
                await _purchaseService.PurchaseAsync(_mapper.Map<AppPurchaseDto>(purchaseRequest));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

/*
{
    "appId": "d24a3c0d-117a-4637-a078-2d386d7a6952",
  "userId": "8f681848-75c5-4c09-8b01-62ab2713b2b2",
  "saveCreditCard": true,
  "creditCard": {
        "name": "Adalto Jarbas Lopes",
    "number": "5496374407457455",
    "cvv": "123",
    "expirationDateMMYYYY": "122025"
  }
}
*/

/*
{
    "userId": "8f681848-75c5-4c09-8b01-62ab2713b2b2",
  "name": "App Teste",
  "price": 120
}
*/