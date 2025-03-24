using Crud_Operation_Task.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Core.IServicesContract
{
    public interface IUserServices
    {


        Task<UserDto> LoginAsync(LoginDto loginDto);

        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> CheckEmailExistAsync(string Email);


        Task<DisplayUserDto> GetUserByEmailAsync(string Email);


    }
}
