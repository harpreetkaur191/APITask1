using System.ComponentModel.DataAnnotations;

namespace APITask1.Models
{
    public class UserRegistrationRequest
    {
        [Required,EmailAddress]
        public string Email {  get; set; }=string.Empty;

        [Required,MinLength(6)]
        public string Password { get; set; } = string.Empty;
        
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public long Phone { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;


    }
}
