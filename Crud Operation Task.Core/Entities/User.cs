using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.Entities
{
    public class User:IdentityUser
    {


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }


    }
}
