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
            try
            {
                _appService.AddApp(_mapper.Map<AppDto>(addAppRequest));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getapps")]
        public IActionResult Get()
        {
            try
            {
                var apps = _appService.GetApps();
                var response = new AppResponse()
                {
                    Apps = _mapper.Map<IEnumerable<App>>(apps)
                };
                return Ok(_mapper.Map<AppResponse>(response));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getappsbyuser/{userid}")]
        public IActionResult GetByUser(string userid)
        {
            try
            {
                var apps = _purchaseService.GetAppsByUser(userid);                

                var response = new AppResponse()
                {
                    Apps = _mapper.Map<IEnumerable<App>>(apps)
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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