using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using Learning_Razor_Pages.Services;

namespace Learning_Razor_Pages.Services.CookiesAuthentication;

public class CookiesAuthentication : ICookiesAuthentication
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CookiesAuthentication(IHttpContextAccessor contextAccessor){
        _httpContextAccessor = contextAccessor;
    }
    public async Task SignInAsync(string emailAddress)
    {
        List<Claim> claims = new List<Claim>(){ new Claim(ClaimTypes.Email, emailAddress) };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}