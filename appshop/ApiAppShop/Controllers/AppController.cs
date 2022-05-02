using ApiAppShop.Application.DataContracts.Requests.App;
using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Services;
using ApiUser.Application.DataContracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Controllers
{
    [ApiController, Authorize(Roles = UserRoleConstants.Guest)]
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
        public async Task<IActionResult> SetAsync(AddAppRequest addAppRequest)
        {
            try
            {
                await _appService.AddAppAsync(_mapper.Map<AppDto>(addAppRequest));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getapps")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var apps = await _appService.GetAppsAsync();
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
        public async Task<IActionResult> GetByUser(string userid)
        {
            try
            {
                var apps = await _purchaseService.GetAppsByUserAsync(userid);                

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