using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.Dtos
{
    public class LoginDto
    {

        [Required(ErrorMessage ="Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password Is Required")]
        public string Password { get; set; }


    }
}
