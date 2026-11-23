using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public class UsersService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : IUsersService
    {
          public async Task<RegisteredUserDTO> RegisterAsync(RegisterUserDTO registerUserDTO)
          {
                var user = new ApplicationUser
                {
                    Email = registerUserDTO.Email,
                    FirstName = registerUserDTO.FirstName,
                    LastName = registerUserDTO.LastName

                };

                var result = await userManager.CreateAsync(user, registerUserDTO.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Bad Credential");
                }

                var registeredUser = new RegisteredUserDTO
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id
                };

                return registeredUser;
          }
        public async Task<string> LoginAsync(LoginUserDTO loginUserDTO)
        {
            var user = await userManager.FindByNameAsync(loginUserDTO.Email);
            if (user == null)
            {
                throw new Exception("Invalid email");
            }

            var isPaswordValid = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);
            if (!isPaswordValid)
            {
                throw new Exception("Incorrect password");
            }

            string message = string.Empty;

            return message = $"User {loginUserDTO.Email} has been login successfully";
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            // Basic claims
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub,user.Id),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Name, user.FullName)
            };

            // Role Claim
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            claims = claims.Union(roleClaims).ToList();

            // Jwt Key

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JWTSetting:Issuer"],
                audience: configuration["JWTSetting:Audience"],
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JWTSetting:DurationInMinutes"])),
                signingCredentials: credential

                );

            return new JwtSecurityTokenHandler().WriteToken(token);            
        }
       
    
    }
}
