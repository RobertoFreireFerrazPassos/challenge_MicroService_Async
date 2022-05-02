using ApiAppShop.Application.DataContracts.Requests.User;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiAppShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService,
            IMapper mapper)
        {
            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(SignInRequest signInRequest)
        {
            try
            {
                await _authService.CreateNewUserAsync(_mapper.Map<UserDto>(signInRequest));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInAsync(LogInRequest signInRequest)
        {
            try
            {
                return Ok(await _authService.LogInAsync(_mapper.Map<LogInDto>(signInRequest)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      
    }
}