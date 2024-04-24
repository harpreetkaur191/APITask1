using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

namespace APITask1
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        //public string? VerificationToken { get; set; }
        //public DateTime? VerifiedAt { get; set; }
        //public string? PasswordResetToken { get; set; }
        //public DateTime? ResetTokenExpires { get; set; }
    }
}
//migrationBuilder.Sql("INSERT INTO Admin_s (Email, Password) VALUES ('harpreet19@gmail.com', 'harpreet1234')");

// Mailgun username harpreetpizone@gmail.com pwd 19mannat01
