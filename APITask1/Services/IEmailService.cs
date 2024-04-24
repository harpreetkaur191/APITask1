using APITask1.Models;

namespace APITask1.Services
{
    public interface IEmailService
    {
        //string DecryptMessage(string encryptedPassword);
        Task SendEmail(string body);
        //change---
        Task<bool> Register(UserRegistrationRequest request);
        Task<Person> Login(UserLoginRequest request);
        Task<string> GenerateRandomPassword(int length = 8);

        //string EncryptPassword(string password);

        //string DecryptedPassword(string password);
        //---

    }
}
