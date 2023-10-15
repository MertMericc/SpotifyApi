using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.PlaylistFollowerDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistFollowerController : ControllerBase
    {
        private IPlaylistFollowerService _playlistFollowerService;

        public PlaylistFollowerController(IPlaylistFollowerService playlistFollowerService)
        {
            _playlistFollowerService = playlistFollowerService;
        }

        [HttpPost("create")]
        public IActionResult AddPlaylist(PlaylistFollowerCreateDto dto)
        {
            var result = _playlistFollowerService.Add(dto);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _playlistFollowerService.GetAll();
            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _playlistFollowerService.Delete(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdatePlayList(PlaylistFollowerUpdateDto dto)
        {
            var result = _playlistFollowerService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getById")]
        public IActionResult GetByIdPlayList(int id)
        {
            var result = _playlistFollowerService.GetById(id);
            return Ok(result);
        }
    }
}
