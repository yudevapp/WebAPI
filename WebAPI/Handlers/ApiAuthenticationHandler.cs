using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Services;

namespace WebAPI.Handlers;

public class ApiAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ApiKeyValidatorService validatorService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)

{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string apiKey = string.Empty;

        if(Request.Headers.TryGetValue(AuthenticationDefaults.ApiKeyHeaderName, out var headerValues))
        {
            apiKey = headerValues.ToString();
        }
        if(string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.NoResult();
        }

        var valid = await validatorService.IsValidKeyAsync(apiKey, Context.RequestAborted);
        if(!valid)
        {
            return AuthenticateResult.Fail("Invalid APi key");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "apiKey"),
            new(ClaimTypes.Name, "ApiKeyClient")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var prensipal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(prensipal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}