using Crud_Operation_Task.Core;
using Crud_Operation_Task.Core.Dtos;
using Crud_Operation_Task.Core.Entities;
using Crud_Operation_Task.Core.IServicesContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Operation_Task.Services.Services
{
    public class UserService : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }



        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return null;

            return new UserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName= user.Email.Split("@")[0],
                IsActive = true,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };



        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {


                if (await CheckEmailExistAsync(registerDto.Email)) return null;


            var user = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                NormalizedEmail=registerDto.Email.ToUpper()
            };


            var result = await _userManager.CreateAsync(user, registerDto.Password);
            //if (!result.Succeeded) return null;

            if (!result.Succeeded)
            {
                // Log or return the errors for debugging
                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
                Console.WriteLine(string.Join(", ", errors)); // Or use your logging system
            }

            if (!await _roleManager.RoleExistsAsync(registerDto.Role)) return null;

            var result2 = await _userManager.AddToRoleAsync(user, registerDto.Role);

            if (!result2.Succeeded) return null;
            return new UserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                IsActive = true,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };


        }


        public async Task<bool> CheckEmailExistAsync(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }


        public async Task<DisplayUserDto> GetUserByEmailAsync(string Email)
        {

            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null) return null;

            var mappedUser = new DisplayUserDto()
            {
                Id=user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                IsActive = user.IsActive
            };

            return mappedUser;

        }

        public async Task<DisplayUserDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {


            var user = await _userManager.FindByIdAsync(updateUserDto.Id);
            if (user == null) return null;

            // Update user details
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.Email.Split("@")[0]; 
            user.NormalizedEmail = updateUserDto.Email.ToUpper();

            // Update password if provided
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, updateUserDto.Password);
                if (!result.Succeeded) return null;
            }

            // Update role
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.FirstOrDefault() != updateUserDto.Role)
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (await _roleManager.RoleExistsAsync(updateUserDto.Role))
                {
                    await _userManager.AddToRoleAsync(user, updateUserDto.Role);
                }
                else
                {
                    return null; 
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) return null;

            await _unitOfWork.CompleteAsync();

            return new DisplayUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = updateUserDto.Role,
                IsActive = user.IsActive
            };



        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return false;

            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
