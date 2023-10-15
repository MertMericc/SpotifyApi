using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.User;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getallusers")]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetList();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getuserbyid")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost("updateuser")]
        public IActionResult UpdateUser(UserUpdateDto dto)
        {
            var result = _userService.Update(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteuser")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.Delete(id);
            return Ok(result);
        }
    }
}
