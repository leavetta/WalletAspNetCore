﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WalletAspNetCore.Models.DTO.Requests;
using WalletAspNetCore.Models.DTO.Responses;
using WalletAspNetCore.Auth;
using WalletAspNetCore.Services.Interfaces;

namespace WalletAspNetCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsersService _userService;
        private readonly JwtParser _jwtParser;

        public UsersController(IAuthService authService, IUsersService userService, JwtParser jwtParser)
        {
            _jwtParser = jwtParser;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest registerUserRequest)
        {
            var userId = await _authService.RegisterAsync(registerUserRequest.Name, registerUserRequest.Email, registerUserRequest.Password);
            
            return Ok(userId);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest loginUserRequest)
        {
            var token = await _authService.LoginAsync(loginUserRequest.Email, loginUserRequest.Password);
            //HttpContext.Response.Cookies.Append("secretCookie", token.Item1);
            
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync()
        {
            try
            {
                var authToken = HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                Guid userId = _jwtParser.ExtractIdUser(authToken) ?? throw new ArgumentNullException();

                var user = await _userService.GetUserByIdAsync(userId);
                var userResponse = new UsersResponse(user.Id, user.Name, user.Email, user.Password);

                return Ok(userResponse);
            }
            catch
            {
                return Unauthorized("Пользователь не залогинился");
            }
        }
    }
}
