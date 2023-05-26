using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        // ctor para un constructror
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetSingle(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        // Post recibe informacion desde el body, y get desde la url
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> AddUser(AddUserDTO newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> UpdateUser(UpdateUserDTO updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
