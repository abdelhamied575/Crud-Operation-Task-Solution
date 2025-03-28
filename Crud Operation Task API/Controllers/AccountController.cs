﻿using Crud_Operation_Task.Core;
using Crud_Operation_Task.Core.Dtos;
using Crud_Operation_Task.Core.Entities;
using Crud_Operation_Task.Core.IServicesContract;
using Crud_Operation_Task_API.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Crud_Operation_Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {


        private readonly IUserServices _userServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public AccountController(IUserServices userServices, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _userServices = userServices;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
        {
            var user= await _userServices.LoginAsync(loginDto);
            if(user is null) return BadRequest("Invalid Login");
            return Ok(user);
        }


        [HttpPost("Register")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userServices.RegisterAsync(registerDto);
            if (user is null) return BadRequest();
            return Ok(user);
        }


        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayUserDto>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var mappedUsers = users.Select(x => new DisplayUserDto()
            {
                Id= x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                IsActive = x.IsActive,
                Role = _userManager.GetRolesAsync(x).Result.FirstOrDefault()

            }).ToList();
            return Ok(mappedUsers);
        }



        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<DisplayUserDto>>GetUser(string email)
        {

            var user= await _userServices.GetUserByEmailAsync(email);
            if (user is null) return NotFound();

            return Ok(user);


        }



        [HttpPut("UpdateUser")]
        [Authorize(Roles = "Admin,Employee,Manager")] 
        public async Task<ActionResult<DisplayUserDto>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var updatedUser = await _userServices.UpdateUserAsync(updateUserDto);
            if (updatedUser == null) return BadRequest("Failed to update user");

            return Ok(updatedUser);
        }



        [HttpDelete("DeleteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var success = await _userServices.DeleteUserAsync(userId);
            if (!success) return NotFound("User not found or deletion failed");

            return Ok(new { Message = "User deleted successfully" });
        }



    }
}
