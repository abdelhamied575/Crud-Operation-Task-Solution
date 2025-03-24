using Crud_Operation_Task.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.IServicesContract
{
    public interface ITokenService
    {

        Task<string>CreateTokenAsync(User user,UserManager<User> userManager);

    }
}
