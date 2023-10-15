using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpPost("create")]
        public IActionResult AddPlaylist(PlaylistCreateDto dto)
        {
            var result = _playlistService.Add(dto);
            return Ok(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _playlistService.GetAll();
            return Ok(result);
        }

        [HttpGet("getallactive")]
        public IActionResult GetAllActive()
        {
            var result = _playlistService.GetAllActive();
            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(string id)
        {
            var result = _playlistService.Delete(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdatePlayList(PlaylistUpdateDto dto)
        {
            var result = _playlistService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getById")]
        public IActionResult GetByIdPlayList(string id)
        {
            var result = _playlistService.GetById(id);
            return Ok(result);
        }



    }
}
