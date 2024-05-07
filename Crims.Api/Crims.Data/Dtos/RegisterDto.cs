using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
