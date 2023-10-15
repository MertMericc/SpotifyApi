using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.SpotifApiDtos;
using System.Collections.Generic;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackPoolsController : ControllerBase
    {
        private readonly ITrackPoolService _trackPoolService;

        public TrackPoolsController(ITrackPoolService trackPoolService)
        {
            _trackPoolService = trackPoolService;
        }

        [HttpGet("getalbums")]
        public async Task<IActionResult> GetAlbums(string token)
        {
            var result = await _trackPoolService.GetNewReleasesAlbums(token);
            return Ok(result);
        }

        [HttpGet("gettracks")]
        public async Task<IActionResult> GetTracks(string token, int pageSize, int pageNumber)
        {
            var result = await _trackPoolService.GetTracks(token, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("gettrackbyid")]
        public async Task<IActionResult> GetTrackById(string trakcId, string token)
        {
            var result = await _trackPoolService.GetTrackById(trakcId, token);
            return Ok(result);
        }
    }
}
