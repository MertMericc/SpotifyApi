using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.Album;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var album = _albumService.GetById(id);
            return Ok(album);
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var albumList = _albumService.GetList();
            return Ok(albumList);
        }

        [HttpPost("add")]
        public IActionResult Add(AlbumAddDto dto)
        {
            var result = _albumService.Add(dto);
            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _albumService.Delete(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult Update(AlbumUpdateDto dto)
        {
            var result = _albumService.Update(dto);
            return Ok(result);
        }
    }
}
