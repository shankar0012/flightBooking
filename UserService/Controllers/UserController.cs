using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {



        public static UserModel user = new UserModel();
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }
       
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserModel request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] PasswordSalt);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = PasswordSalt;
           // save data into the databse
            return Ok(user);
        }
        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserModel request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User Not Found");
            }
            if (!verifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("password Not match");
            }
            string token = createToken(user);
            return Ok(token);


        }
        private string createToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            //here we are returing jwttoken
            return jwt;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        [HttpGet(Name = "Search") , Authorize(Roles = "Admin")]
        public async Task<ActionResult<Ticket>> Search(string emailId,string PNR)
        {
            Ticket tkt = new Ticket();
            if (PNR!=null)
            {
                //get data from DB
                tkt.PNR = "abc1";
                tkt.FullName = "Shankar test data";
                tkt.BookedDate = System.DateTime.Now.AddDays(-1);
                tkt.No_Of_Seats = 2;
                return Ok(tkt);
            }
            else if (emailId!=null)
            {
                tkt.PNR = "abc1";
                tkt.FullName = "Shankar test data";
                tkt.BookedDate = System.DateTime.Now.AddDays(-1);
                tkt.No_Of_Seats = 2;
                return Ok(tkt);
              
            }
            else
            {
                return BadRequest("Data not found");
            }
            


        }
        //[HttpGet]
        //public string test()
        //{
        //    return "tt";
        //}
        
    }
}
