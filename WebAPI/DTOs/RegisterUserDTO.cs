using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class RegisterUserDTO 
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, MaxLength(70)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(70)]
        public string FirstName { get; set; } = string.Empty;
    }
    public class LoginUserDTO()
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
    public class RegisteredUserDTO()
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
