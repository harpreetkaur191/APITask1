

//using MailKit.Net.Smtp;
//using MailKit.Security;
//using MimeKit;
//using MimeKit.Text;

//namespace APITask1.Services
//{
//    public class EmailService : IEmailService
//    {


//        public async Task SendEmail(string body)

//        {

//            try
//            {
//                // Creating email message
//                var email = new MimeMessage();
//                email.From.Add(MailboxAddress.Parse("harpreetbhatia1917@gmail.com"));
//                email.To.Add(MailboxAddress.Parse("harpreetbhatia1917@gmail.com"));
//                email.Subject = "Test Email Subject";
//                email.Body = new TextPart(TextFormat.Html) { Text = body };

//                // Send email
//                using (var smtp = new SmtpClient())
//                {
//                    await smtp.ConnectAsync("smtp.mailgun.org", 587, SecureSocketOptions.StartTls);
//                    await smtp.AuthenticateAsync("postmaster@sandbox3e3c3c8b94c3443f88059ca6fd245ac8.mailgun.org", "e36f17f0d7a43aec11219602aac8a857-19806d14-67cf360a");
//                    await smtp.SendAsync(email);
//                    await smtp.DisconnectAsync(true);
//                }

//            }
//            catch (Exception) { }
//            }


//    }
//    }

// correct code for service class----

using APITask1.Models;
using Braintree;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace APITask1.Services
{
    public class EmailService : IEmailService


    {
        private readonly DataContext _context;
      

        public EmailService(DataContext context)
        {
            _context = context;
          
        }
       
        public async Task<bool> AddPerson(UserRegistrationRequest request)
        {
            if (_context.Persons.Any(u => u.Email == request.Email))
            {
                return false; // User already exists
            }

            var person = new Person
            {
                Id = Guid.NewGuid(),            
                Email = request.Email,
                Password = EncryptPassword(request.Password),
             
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            await SendEmail("Welcome");

            return true;
        }
       
        public async Task<string> Login(UserLoginRequest request)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(
                u => u.Email == request.Email && u.Email == request.Email);

            if (person == null || DecryptedPassword(person.Password) != request.Password)
            {
                var messg = "Password does not match";
                return messg.ToString();
            }
            if(person == null || DecryptedPassword(person.Email) != request.Email) 
            {
                var msg = "Username does not match";
                return msg.ToString();
            }
            var result = "Welcome User";
            return result.ToString();


        }

        public async Task<string> GetPersons(Pagination pagination)
        {
                var query = _context.Persons.AsQueryable();
                int totalItems = await query.CountAsync();

                // Apply pagination
                var paginatedItems = await query
                    .Skip((pagination.PageNumber - 1) * pagination.ValidatedPageSize)
                    .Take(pagination.ValidatedPageSize)
                    .ToListAsync();

                var result = new 
                {
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / pagination.ValidatedPageSize),
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.ValidatedPageSize,
                    Items = paginatedItems
                };
            string jsonResult = JsonConvert.SerializeObject(result);
            return jsonResult;

        }
        public string DecryptMessage(string encryptedPassword)
        {
            throw new NotImplementedException();
        }
        public async Task SendEmail(string body)
        {
            try
            {
                // Creating email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("harpreetbhatia1917@gmail.com"));
                email.To.Add(MailboxAddress.Parse("harpreetbhatia1917@gmail.com"));
                email.Subject = "Test Encrypted Email Subject";

                email.Body = new TextPart(TextFormat.Html) { Text = body }; //encryptBody

                // Send email
                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync("smtp.mailgun.org", 587, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync("postmaster@sandbox3e3c3c8b94c3443f88059ca6fd245ac8.mailgun.org", "e36f17f0d7a43aec11219602aac8a857-19806d14-67cf360a");
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
       
        public static string EncryptPassword(string password)
        {
            byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
            string encryptedPassword = Convert.ToBase64String(storePassword);
            return encryptedPassword;
        }
        public static string DecryptedPassword(string password)
        {
            byte[] encryptedPassword = Convert.FromBase64String(password);
            string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
            return decryptedPassword;
        }

        public Task<string> GenerateRandomPassword(int length = 8)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(UserRegistrationRequest request)
        {
            throw new NotImplementedException();
        }

        Task<Person> IEmailService.Login(UserLoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
