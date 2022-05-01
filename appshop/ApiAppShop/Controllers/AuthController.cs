using ApiAppShop.Application.DataContracts.Requests.User;
using ApiAppShop.Domain.Dtos.User;
using ApiAppShop.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpGet("LogOff")]
        public IActionResult LogOff()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(SignInRequest signInRequest)
        {
            try
            {
                _authService.CreateNewUser(_mapper.Map<UserDto>(signInRequest));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn(LogInRequest signInRequest)
        {
            try
            {
                return Ok(_authService.LogIn(_mapper.Map<LogInDto>(signInRequest)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      
    }
}