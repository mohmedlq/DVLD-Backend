using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        #region Get

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }


        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserDto>> GetById(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpGet("person/{personId:int}")]
        public async Task<ActionResult<UserDto>> GetByPersonId(
            int personId)
        {
            var user = await _userService.GetByPersonIdAsync(personId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDto>> GetByUsername(
            string username)
        {
            var user = await _userService.GetByUsernameAsync(username);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        #endregion


        #region Authentication

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(
            string username,
            string password)
        {
            var user = await _userService.LoginAsync(
                username,
                password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(user);
        }

        #endregion


        #region Create

        [HttpPost]
        public async Task<ActionResult<int>> Create(
            CreateUserRequest request)
        {
            try
            {
                var userId =
                    await _userService.CreateAsync(request);

                return Ok(userId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        #endregion


        #region Update

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> Update(
            int userId,
            UpdateUserRequest request)
        {
            var updated =
                await _userService.UpdateAsync(
                    userId,
                    request);

            if (!updated)
                return BadRequest(
                    "User could not be updated.");

            return NoContent();
        }

        #endregion


        #region Change Password

        [HttpPut("{userId:int}/password")]
        public async Task<IActionResult> ChangePassword(
            int userId,
            ChangePasswordRequest request)
        {
            var changed =
                await _userService.ChangePasswordAsync(
                    userId,
                    request);

            if (!changed)
                return BadRequest(
                    "Password could not be changed.");

            return NoContent();
        }

        #endregion


        #region Check Existence

        [HttpGet("{userId:int}/exists")]
        public async Task<ActionResult<bool>> IsExist(
            int userId)
        {
            var exists =
                await _userService.IsExist(userId);

            return Ok(exists);
        }

        #endregion


        #region Delete

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var deleted =
                await _userService.DeleteAsync(userId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        #endregion
    }
}