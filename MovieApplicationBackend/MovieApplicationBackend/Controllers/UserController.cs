using Microsoft.AspNetCore.Mvc;
using MovieAppRepository.Model;
using MovieAppServices.IServices;

namespace MovieApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;
        public UserController(IUserServices userServices)
        {
            _userService = userServices;
        }

        [HttpPost("UserAdd")]
        public IActionResult AddUser([FromBody] UserModel userAddModel)
        {
            var result = _userService.AddUser(userAddModel);
            return Ok(result);
        }

        [HttpGet("Country")]
        public IActionResult GetCountries()
        {
            try
            {
                var result = _userService.GetCountries();

                if (result == null || !result.Any())
                {
                    return NotFound("No countries found.");
                }

                return Ok(result);
            }     
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _userService.GetUsers();

                if (result == null || !result.Any())
                {
                    return NotFound("No countries found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPatch("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UserModel request)
        {
            try
            {
                var result = await _userService.UpdateUserStatusAsync(request.CustomerId, request.Active);

                if (!result)
                {
                    return NotFound("User not found or update failed.");
                }

                return Ok(new { message = "User status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}
