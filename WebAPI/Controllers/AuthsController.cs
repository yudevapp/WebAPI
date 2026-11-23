using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.NativeInterop;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthsController(IUsersService usersService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisteredUserDTO>> Register(RegisterUserDTO registerUserDTO)
        {
            var user = await usersService.RegisterAsync(registerUserDTO);

            return Ok($"User : {user.Email} has been successfully registered");
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            var user = await usersService.LoginAsync(loginUser);
            return Ok();
        }

    }
}
