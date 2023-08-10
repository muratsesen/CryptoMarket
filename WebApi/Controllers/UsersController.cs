using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public UsersController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewUser([FromBody] NewUser newUserRequest)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreateUserRequest(newUserRequest));
            if (isSuccessful)
            {
                return Ok("User created succesffully.");
            }
            return BadRequest("Failed to create user");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser updateUser)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdateUserRequest(updateUser));
            if (isSuccessful)
            {
                return Ok("User updated successfully.");
            }
            return NotFound("User does not exists.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserDto userDto = await _mediatrSender.Send(new GetUserByIdRequest(id));
            if (userDto != null)
            {
                return Ok(userDto);
            }
            return NotFound("User does not exists.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            List<UserDto> userDtos = await _mediatrSender.Send(new GetUsersRequest());
            if (userDtos != null)
            {
                return Ok(userDtos);
            }
            return NotFound("No Users were found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeleteUserRequest(id));
            if (isSuccessful)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User doest not exists.");
        }
    }
}
