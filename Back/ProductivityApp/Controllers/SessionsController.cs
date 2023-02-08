using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityApp.Dtos.UserSessionDtos;
using ProductivityApp.Services.Interfaces;
using ProductivityApp.Shared.CustomUserSessionExceptions;
using ProductivityApp.Shared.CustomUserExceptions;
using ProductivityApp.Shared.ServerExceptions;
using System.Security.Claims;

namespace ProductivityApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionsController : ControllerBase
    {
        private readonly IUserSessionService _UserSessionService;

        public UserSessionsController(IUserSessionService UserSessionService)
        {
            _UserSessionService = UserSessionService;
        }

        [HttpGet("getAllUserSessions")]
        public async Task<ActionResult<List<UserSessionDto>>> GetAllUserSession()
        {
            try
            {
                //get the role claim from token 
                var userId = GetAuthorizedUserId();
                return Ok(await _UserSessionService.GetAllUserSessions(userId));
            }
            catch (InternalServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSessionDto>> GetUserSessionById(int id)
        {
            try
            {
                return Ok(await _UserSessionService.GetUserSessionById(id));
            }
            catch (UserSessionNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InternalServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("addUserSession")]
        public async Task<IActionResult> AddUserSession([FromBody] AddUserSessionDto addUserSessionDto)
        {
            try
            {
                var userId = GetAuthorizedUserId();
                await _UserSessionService.AddUserSession(addUserSessionDto,userId);
                return StatusCode(StatusCodes.Status201Created, "New UserSession was added");
            }
            catch (UserSessionDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("updateUserSession")]
        public async Task<IActionResult> UpdateUserSession([FromBody] UpdateUserSessionDto updateUserSessionDto)
        {
            try
            {
                await _UserSessionService.UpdateUserSession(updateUserSessionDto);
                return StatusCode(StatusCodes.Status204NoContent, $"{updateUserSessionDto.Id} was updated!");
            }
            catch (UserSessionNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UserSessionDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSession(int id)
        {
            try
            {
                await _UserSessionService.DeleteUserSession(id);
                return StatusCode(StatusCodes.Status204NoContent, $"UserSession with id {id} was successfully deleted!");
            }
            catch (UserSessionNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InternalServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string? name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new UserNotFoundException(
                    "Name identifier claim does not exist!");
            }
            return userId;
        }
    }
}
