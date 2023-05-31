using Microsoft.AspNetCore.Mvc;
using ProductAPi.Authenticate;
using ProductAPi.DataAccess;
using ProductAPi.Dtos;
using ProductAPi.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProductAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ProductAPiContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(ProductAPiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("Register")]
        public ActionResult<Customer> RegisterCustomer(CustomerDtos request)
        {
            CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
            Customer customer = new Customer()
            {
                FirstName = request.FirstName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PasswordHash =passwordHash,
                PasswordSalt = passwordSalt,               
                Gender = request.Gender
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(LoginDtos request) //username:tonystark pswd:12345
        {
            Customer? checkvalid = _context.Customers.FirstOrDefault(customer => customer.UserName == request.UserName);
            if(checkvalid == null)
            {
                return BadRequest("User not found");
            }
            if(!VerifyPasswordHash(request.Password, checkvalid.PasswordHash, checkvalid.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            else
            {
                var JwtSettings = _configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();

                var token = JwtTokenHelper.GenerateToken(JwtSettings, checkvalid);

                HttpContext.Session.SetString("Token", token);
                return Ok(token);
            }
        }
        private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                byte[] hashbytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(hashbytes);
            }
        }

        private static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(passwordSalt)))
            {
                var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedhash.SequenceEqual(Convert.FromBase64String(passwordHash));
            }
        }
    }
    
}
