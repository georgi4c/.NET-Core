using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Models.Users
{
    public class UserEditModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6)]
        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6)]
        [Required]
        public string CurrentPassword { get; set; }


    }
}
