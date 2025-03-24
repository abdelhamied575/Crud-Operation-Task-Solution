using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.Dtos
{
    public class UserDto
    {

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }

        public bool IsActive { get; set; }

        public string Token { get; set; }


    }
}
