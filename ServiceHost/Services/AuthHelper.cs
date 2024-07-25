using Framework.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ServiceHost.Services
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void SignIn(AuthViewModel command)
        {
            List<Claim> claims = new()
            {
                new Claim("AccountId",command.AccountId.ToString()),
                new Claim(ClaimTypes.Name,command.FullName),
                new Claim(ClaimTypes.Role,command.Role.Id.ToString()),
                new Claim("Username",command.UserName),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var auth_properties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            };
            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), auth_properties);
        }
        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Claims.Any(x => x.Type == "AccountId");
        }

        public string GetRoleId()
        {
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        }

        public AuthViewModel GetCurrentAccountAuthViewModel()
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            if (claims.Count == 0)
            {
                return null;
            }
            var result = new AuthViewModel(long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value), long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value)
                , claims.FirstOrDefault(x => x.Type == "Username").Value, claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);
            return result;
        }
    }
}