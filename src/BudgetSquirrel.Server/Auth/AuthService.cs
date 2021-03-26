using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Server.Biz.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BudgetSquirrel.Server.Auth
{
  public class AuthService : IAuthService
  {
    private readonly IAccountRepository accountRepository;
    private readonly ICryptor cryptor;
    private readonly GateKeeperConfig gateKeeperConfig;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthService(IAccountRepository accountRepository, ICryptor cryptor, GateKeeperConfig gateKeeperConfig, IHttpContextAccessor httpContextAccessor)
    {
      this.accountRepository = accountRepository;
      this.cryptor = cryptor;
      this.gateKeeperConfig = gateKeeperConfig;
      this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Account> Authenticate(LoginRequest credentials)
    {
      Account user = await this.accountRepository.GetByEmail(credentials.Username);

      if (user != null)
      {
          bool isPasswordCorrect = await this.accountRepository.IsPasswordAttemptCorrect(user.Email, credentials.Password);
          if (!isPasswordCorrect)
          {
            throw new AuthenticationException("Password is incorrect");
          }
      }
      return user;
    }

    public Task<Account> GetCurrentUser()
    {
      throw new System.NotImplementedException();
    }

    public async Task SignInAsync(Account user)
    {
      await this.httpContextAccessor.HttpContext.SignOutAsync();
      
      ClaimsIdentity claimsIdentity = new ClaimsIdentity(CreateUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);

      await this.httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
          new ClaimsPrincipal(claimsIdentity),
          new AuthenticationProperties { IsPersistent = true });
    }

    /// <summary>	
    /// Is an extension method on the user class <see cref="User"/>	
    /// will set up the claims to be added to the cookie so we can 	
    /// get user data in other requests <see cref="GetUserFromClaims"/>	
    /// </summary>	
    /// <param name="user">Extension method on the user</param>	
    /// <returns>A list of claims to be added to the cookie</returns>	
    private List<Claim> CreateUserClaims(Account user)
    {
      var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email)
        };

      return claims;
    }

    /// <summary>	
    /// Will get base User data from the cookie we can correctly retrieve their	
    /// data from the database	
    /// </summary>	
    /// <param name="userClaims">The list of user claims attached to the cookie</param>	
    /// <returns>The user with base user data</returns>	
    private Guid GetUserIdFromClaims(IEnumerable<Claim> userClaims)
    {
      return Guid.Parse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
    }
  }
}