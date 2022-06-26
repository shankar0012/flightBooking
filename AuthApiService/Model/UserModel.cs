using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiService.Model
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        public string EmailId { get; set; } = string.Empty;
        [MaxLength(20)]
        public string FullName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
    }
}
