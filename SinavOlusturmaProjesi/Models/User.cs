using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.Models
{
    public class User
    {
        private string username;
        private string password;
        [Required]
        public string Username { get => username; set => username = value; }
        [Required]
        public string Password { get => password; set => password = value; }
    }
}
