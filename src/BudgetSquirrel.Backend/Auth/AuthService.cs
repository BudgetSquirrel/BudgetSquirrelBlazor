using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Domain.Accounts;
using BudgetSquirrel.BudgetPlanning.Business.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BudgetSquirrel.Backend.Auth
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
      string email = this.GetUserEmailFromClaims(this.httpContextAccessor.HttpContext.User.Claims);
      if (email == null)
      {
        return null;
      }
      return this.accountRepository.GetByEmail(email);
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
    /// Get the email of the user who is currently logged in.
    /// </summary>	
    /// <param name="userClaims">The list of user claims attached to the cookie</param>	
    /// <returns>The email of the logged in user</returns>	
    private string GetUserEmailFromClaims(IEnumerable<Claim> userClaims)
    {
      return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
    }
  }
}