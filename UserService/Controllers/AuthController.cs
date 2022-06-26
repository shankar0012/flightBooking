using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
            private readonly IConfiguration _config;
            public IUserRepository userRep;
        public static UserEntity user = new UserEntity();
        public AuthController(IConfiguration config, IUserRepository emprepo)
            {
                _config = config;
                userRep = emprepo;
            }
        
        [HttpPost("Register")]
        public async Task<ResponceEntity> Register(User request)
        {
            ResponceEntity re = new ResponceEntity();
            try
            {




                UserModel user = new UserModel();
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] PasswordSalt);
                user.Username = request.UserName;
                user.Password = request.Password;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = PasswordSalt;
                user.EmailId = request.EmailId;
                user.FullName = request.FullName;
                user.IsAdmin = false;

                bool insert = userRep.RegisterUser(user);
                if (insert)
                {
                    re.StatusCode = 200;
                    re.Message = "User Registred Succesfully";
                    return re;
                    // return Ok(user);
                }
                else
                {
                    re.StatusCode = 201;
                    re.Message = "User Alreday Exists";
                    return re;
                }
            }
            catch(Exception ex)
            {
                re.StatusCode = 417;
                re.Message = ex.ToString();
                return re;
            }

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
        public async Task<ResponceEntity> Login(User request)
        {
            ResponceEntity re = new ResponceEntity();
            try
            {
                UserModel usr = new UserModel();
                usr.Username = request.UserName;
                usr.Password = request.Password;
                UserModel user = userRep.GetRegisterUser(usr);
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] PasswordSalt);
                if (user.Username.ToLower() != request.UserName.ToLower())
                {
                    re.StatusCode = 201;
                    re.Message = "User Not Found";
                    return re;
                   
                }
                if (!verifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    re.StatusCode = 201;
                    re.Message = "password Not match";
                    return re;
                          
                }
            
                re.StatusCode = 200;
                re.Message = createToken(user);
                return re;
                
            }
            catch(Exception ex)
            {
                re.StatusCode = 100;
                re.Message = "Error Occured";
                return re;
            }


        }
        private string createToken(UserModel user)
        {
            string isadmin = "";
            if (user.Username.ToLower() == "Admin")
            {
                isadmin = "Admin";
            }
            else
            {
                isadmin = "User";
            }
            List<Claim> claims = new List<Claim>()
            {
               
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,isadmin),
                new Claim("UserRole",isadmin)
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
        [HttpGet(Name = "Search1"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Ticket>> Search(string emailId, string PNR)
        {
            Ticket tkt = new Ticket();
            if (PNR != null)
            {
                //get data from DB
                tkt.PNR = "abc1";
                tkt.FullName = "Shankar test data";
                tkt.BookedDate = System.DateTime.Now.AddDays(-1);
                tkt.No_Of_Seats = 2;
                return Ok(tkt);
            }
            else if (emailId != null)
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

        [HttpPost("Refreshtoken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshtoken = Request.Cookies["RefreshToken"];
            if (!user.RefreshToken.Equals(refreshtoken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if (user.TokenExpries < DateTime.Now)
            {
                return Unauthorized("Token Expired");
            }
            else
            {
                UserModel u = new UserModel();
                    u.Username = user.UserName;
                string Token = createToken(u);
                var Newrefreshtoken = GenerateRefreshToken();
                SetRefreshToken(Newrefreshtoken);
                return Ok(Token);
            }
        }
        private void SetRefreshToken(RefreshToken newrefreshtoken)
        {
            var cookiesOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = newrefreshtoken.Expires
            };
            Response.Cookies.Append("RefreshToken", newrefreshtoken.Token, cookiesOption);
            user.RefreshToken = newrefreshtoken.Token;
            user.TokenCreated = newrefreshtoken.Created;
            user.TokenExpries = newrefreshtoken.Expires;
        }
        private RefreshToken GenerateRefreshToken()
        {

            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = new RefreshToken
            {

                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            return refreshToken;
        }
        [HttpPut("CancelTicket")]
        public IActionResult CancelTicket(string PNR)
        {

            //UserProducer.consumer("ConsumerQueue");
            UserProducer.FlightPublish("TicketCancelByPNRQueue", PNR);
            // save data into the databse
            return Ok("Data Updated Successfully");
            //}
            //else
            //{
            //    return BadRequest("Airline Not Updated Please try again");
            //}

        }
        [HttpGet("GetUserDetailsbyUserName")]
        public IActionResult GetUserDetailsbyUserName(string UserName)
        {

            UserModel um = new UserModel();
            um.Username = UserName;
            UserModel UserDetails = userRep.GetRegisterUser(um);
            if (UserDetails != null)
            {
                // save data into the databse
                return Ok(UserDetails);
            }
            else
            {
                return BadRequest("Data Not Found");
            }

        }

    }
}
