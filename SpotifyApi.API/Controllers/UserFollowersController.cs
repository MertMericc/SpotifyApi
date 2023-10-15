using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.User;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowersController : ControllerBase
    {
        private IUserFollowerService _userFollowerService;

        public UserFollowersController(IUserFollowerService userFollowerService)
        {
            _userFollowerService = userFollowerService;
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getfollowersbyuser")]
        public IActionResult GetFollowersByUser(int userId)
        {
            var result = _userFollowerService.GetFollowersByUserId(userId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpGet("getusersbyfollower")]
        public IActionResult GetUsersByFollower(int followerId)
        {
            var result = _userFollowerService.GetUsersByFollowerId(followerId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost("follow")]
        public IActionResult Follow(UserFollowerCreateDto dto)
        {
            var result = _userFollowerService.Create(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost("unfollow")]
        public IActionResult UnFollow(int id)
        {
            var result = _userFollowerService.Delete(id);
            return Ok(result);
        }
    }
}
