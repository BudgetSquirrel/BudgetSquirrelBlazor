using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Server.Auth;
using BudgetSquirrel.Web.Common.Messages.Auth;
using GateKeeper.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BudgetSquirrel.Server.Controllers
{
    [ApiController]
    [Route("backend/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> logger;

        private readonly IAuthService authenticationService;
        private readonly IAccountService accountService;
        private readonly ILoginUserRepository userRepository;

        public AuthController(ILogger<AuthController> logger,
                                        IAuthService authenticationService,
                                        IAccountService accountService,
                                        ILoginUserRepository userRepository)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            LoginUser userData = await this.authenticationService.GetCurrentUser();
            CurrentUserResponse user = AuthConverter.ToApiMessage(userData);
            return new JsonResult(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest credentials)
        {
            try
            {
                LoginUser user = await this.authenticationService.Authenticate(credentials);

                if (user != null)
                {
                    await this.authenticationService.SignInAsync(user);
                    return Ok();
                }
                
                return this.BadRequest("Username or Password were incorrect");
            }
            catch (Exception ex) when (ex is AuthenticationException)
            {
                return this.BadRequest("Username or Password were incorrect");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest newUser)
        {
            await this.accountService.CreateUser(newUser);

            LoginUser user = await this.userRepository.GetByEmail(newUser.Email);
            await this.authenticationService.SignInAsync(user);

            if (user == null)
            {
                return this.StatusCode(500, "There was an error creating the account, please try again.");
            }
            return Ok();

        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.Ok();
        }
    }
}
