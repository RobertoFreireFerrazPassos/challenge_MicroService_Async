using ApiAppShop.Application.DataContracts.Requests.User;
using ApiAppShop.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiAppShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("LogOff")]
        public IActionResult LogOff()
        {
            return Ok();
        }

        [HttpGet("SignIn")]
        public IActionResult SignIn(SignInRequest signInRequest)
        {
            return Ok();
        }

        [HttpGet("LogIn")]
        public IActionResult LogIn()
        {
            return Ok();
        }      
    }
}