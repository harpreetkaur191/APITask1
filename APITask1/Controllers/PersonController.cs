using APITask1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using APITask1.Models;
using System.Reflection.Metadata.Ecma335;

namespace APITask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        private readonly EmailService _eservice;
       
        public PersonController(DataContext context, IEmailService emailService, EmailService eservice)
        {
            _context = context;
            _emailService = emailService;
            _eservice = eservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons([FromQuery] Pagination pagination)
        {
            var result = await _eservice.GetPersons(pagination);

            return Ok(result);
        }

       
        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person per)
        {
            var randomPassword = GenerateRandomPassword();
            per.Password = EmailService.EncryptPassword(randomPassword);
            per.Id = Guid.NewGuid();
            _context.Persons.Add(per);
            await _context.SaveChangesAsync();

            // Send email to per.email with per.password
            string emailBody = $"Hello , your password is: {randomPassword}";

            try
            {
                await _emailService.SendEmail(emailBody);
                return Ok(EmailService.DecryptedPassword(per.Password));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(u => u.Email == request.Email);

            //if (person != null)
            //{
            //    bool isValid = (person.Email == request.Email && EmailService.DecryptedPassword(person.Password) == request.Password);
            //    if (isValid)
            //    {
            //        //return Ok(EmailService.Equals(person.Email, request.Email));
            //        return Ok("logged in");
            //    }
            //    return Ok($"wrong password");
            //}
            //else
            //{
            //    return BadRequest("User not found");

            //}
            return Ok(person);
        }
        
        private string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }

    }
}