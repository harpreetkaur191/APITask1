using System.ComponentModel.DataAnnotations;

namespace APITask1
{
    public class AdminLoginRequest
    {

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
      
    }
}

