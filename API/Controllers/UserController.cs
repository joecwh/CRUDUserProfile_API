using API.Services.UserServices;
using API.Services.UserServices.Models;
using API.Shared.SharedModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserListDetailsResponseModel>> GetUserList()
        {
            var response = await _userService.GetUserListAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsResponseModel>> GetUser(int id)
        {
            var response = await _userService.GetUserAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResultModel>> CreateNewUser(UserRegistrationRequestModel model)
        {
            var response = await _userService.CreateNewUserAsync(model);
            return response.Success ? Ok() : BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultModel>> PutProduct(int id, UpdateUserRequestModel model)
        {
            var response = await _userService.UpdateUserAsync(id, model);
            return response.Success ? Ok() : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteProduct(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return response.Success ? Ok() : BadRequest(response.Message);
        }
    }
}
