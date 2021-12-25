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
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
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
                _userService.SetUser(_mapper.Map<UserDto>(signInRequest));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("LogIn/{userId}")]
        public IActionResult LogIn(string userId)
        {
            try
            {
                return Ok(_userService.GetUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      
    }
}