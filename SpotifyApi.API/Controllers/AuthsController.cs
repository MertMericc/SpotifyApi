using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.User;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto dto)
        {
            var result = _authService.Login(dto);
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto dto)
        {
            var result = _authService.Register(dto);
            return Ok(result);
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword(UserPasswordChangeDto dto)
        {
            var result = _authService.ChangeUserPassword(dto);
            return Ok(result);
        }
    }
}
