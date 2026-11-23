using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Contracts
{
    public interface IUsersService
    {
        Task<string> LoginAsync(LoginUserDTO loginUserDTO);
        Task<RegisteredUserDTO> RegisterAsync(RegisterUserDTO registerUserDTO);
    }
}