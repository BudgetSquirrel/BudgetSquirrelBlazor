using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Server.Auth;
using BudgetSquirrel.Server.Dal.LocalDb.Accounts;
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
        private readonly IAccountRepository accountRepository;

        public AuthController(/*ILogger<AuthController> logger,
                                        IAuthService authenticationService,*/
                                        IAccountRepository userRepository)
        {
            // this.logger = logger;
            // this.authenticationService = authenticationService;
            this.accountRepository = userRepository;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            LoginUser userData = await this.authenticationService.GetCurrentUser();
            CurrentUserResponse user = AuthMessageResolver.ToApiMessage(userData);
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
            await this.accountRepository.CreateUser(newUser);

            // LoginUser user = await this.accountRepository.GetByEmail(newUser.Email);
            // await this.authenticationService.SignInAsync(user);

            // if (user == null)
            // {
            //     return this.StatusCode(500, "There was an error creating the account, please try again.");
            // }
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
