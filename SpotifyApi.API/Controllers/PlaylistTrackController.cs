using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.PlaylistTrackDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistTrackController : ControllerBase
    {
        private IPlaylistTrackService _playlistTrackService;

        public PlaylistTrackController(IPlaylistTrackService playlistTrackService)
        {
            _playlistTrackService = playlistTrackService;
        }

        [HttpPost("create")]
        public IActionResult AddPlaylist(PlaylistTrackCreateDto dto)
        {
            var result = _playlistTrackService.Add(dto);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll(string token)
        {
            var result = _playlistTrackService.GetAll(token);
            return Ok(result);
        }

        [HttpGet("getallbyplaylistid")]
        public IActionResult GetAllByPlaylistId(string playlistId,string token)
        {
            var result = _playlistTrackService.GetAllByPlaylistId(playlistId,token);
            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _playlistTrackService.Delete(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdatePlayList(PlaylistTrackUpdateDto dto)
        {
            var result = _playlistTrackService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getById")]
        public IActionResult GetByIdPlayList(int id,string token)
        {
            var result = _playlistTrackService.GetById(id,token);
            return Ok(result);
        }
    }
}
