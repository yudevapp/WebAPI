using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Services;

namespace WebAPI.Handlers;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    UsersService userService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)

{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if(!Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }

        var authHeader = authHeaderValues.ToString();
        if(string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.NoResult();
        }

        var token = authHeader["Basic ".Length..].Trim();
        string decoded;

        try
        {
            var credentialByte = Convert.FromBase64String(token);
            decoded = Encoding.UTF8.GetString(credentialByte);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Basic authentication token.");
        }

        var separetedIndex = decoded.IndexOf(':');
        if(separetedIndex <= 0)
        {
            return AuthenticateResult.Fail("Invalid Basic authentication credential format.");
        }

        var userNameOrEmail = decoded[..separetedIndex];
        var password = decoded[(separetedIndex + 1)..];

        var loginDTO = new LoginUserDTO
        {
            Email = userNameOrEmail,
            Password = password
        };

        var result = await userService.LoginAsync(loginDTO);
        if(result.Contains("Invalid"))
        {
            return AuthenticateResult.Fail("Invalid username or password");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userNameOrEmail),
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var prensipal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(prensipal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}