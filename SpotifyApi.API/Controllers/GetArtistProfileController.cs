using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.ArtistListDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetArtistProfileController : ControllerBase
    {
        private readonly IArtistListService _artistListService;

        public GetArtistProfileController(IArtistListService artistListService)
        {
            _artistListService = artistListService;
        }

        [HttpGet("artistGet")]
        public async Task<IActionResult> GetArtist(string id,string token)
        {
            var result = await _artistListService.GetById(id,token);
            return Ok(result);
        }

        [HttpGet("artistRelated")]
        public async Task<IActionResult> GetArtistRelated(string id, string token)
        {
            var result = await _artistListService.GetRelatedArtistById(id,token);
            return Ok(result);
        }
    }
}
