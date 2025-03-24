using Crud_Operation_Task.Repository.Context;
using Crud_Operation_Task_API.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Operation_Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BuggyController : ControllerBase
    {

        private readonly AppDbContext _context;

        public BuggyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] // Get : /api/Buggy/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var user = await _context.Users.FindAsync(100000000);

            if (user is null) return NotFound(new ApiErrorResponse(404, "User Not Found"));

            return Ok(user);

        }

        [HttpGet("servererror")] // Get : /api/Buggy/servererror
        public async Task<IActionResult> GetServerRequestError()
        {
            var user = await _context.Users.FindAsync(1000000);

            var userToString = user.ToString(); // Will Throw Exception (Null Reference Exception )

            return Ok(userToString);

        }


        [HttpGet("badrequest")] // Get : /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));

        }

        [HttpGet("badrequest/{id}")] // Get : /api/Buggy/badrequest/ahmed
        public async Task<IActionResult> GetBadRequestError(int id) // Validation Error
        {

            return Ok();

        }

        [HttpGet("unathorized")] // Get : /api/Buggy/unathorized
        public async Task<IActionResult> GetUnathorizedError() // Validation Error
        {
            return Unauthorized(new ApiErrorResponse(401));

        }







    }
}
