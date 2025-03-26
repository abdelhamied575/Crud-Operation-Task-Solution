using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.Dtos
{
    public class UpdateUserDto
    {

        public string Id { get; set; } // Required to identify the user

        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role Is Required")]
        public string Role { get; set; }

        public string? Password { get; set; }

    }
}
