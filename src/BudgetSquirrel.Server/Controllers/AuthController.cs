using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Server.Auth;
using BudgetSquirrel.Server.Biz.Accounts;
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
        private readonly IJwtTokenAuthenticator tokenAuthenticator;

        public AuthController(/*ILogger<AuthController> logger,*/
            IAuthService authenticationService,
            IAccountRepository userRepository,
            IJwtTokenAuthenticator tokenAuthenticator)
        {
            // this.logger = logger;
            this.authenticationService = authenticationService;
            this.accountRepository = userRepository;
            this.tokenAuthenticator = tokenAuthenticator;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Account userData = await this.authenticationService.GetCurrentUser();
            CurrentUserResponse user = AuthMessageResolver.ToApiMessage(userData);
            return new JsonResult(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest credentials)
        {
            try
            {
                Account user = await this.authenticationService.Authenticate(credentials);

                if (user != null)
                {
                    string token = this.tokenAuthenticator.GenerateToken(user.Email);
                    return Ok(token);
                }
                
                return this.Unauthorized("Username or Password were incorrect");
            }
            catch (Exception ex) when (ex is AuthenticationException)
            {
                return this.Unauthorized("Username or Password were incorrect");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest newUser)
        {
            CreateAccountCommand cmd = new CreateAccountCommand(
                this.accountRepository,
                (newUser.Email,
                newUser.Password,
                newUser.ConfirmPassword,
                newUser.FirstName,
                newUser.LastName));
            await cmd.Execute(await cmd.Validate(await cmd.Load()));
            
            return Ok();

        }

        [AllowAnonymous]
        [HttpOptions("register")]
        public IActionResult RegisterOptions()
        {
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
