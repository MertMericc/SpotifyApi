using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.FavouriteDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;

        public FavouritesController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [HttpPost("create")]
        public IActionResult Add(FavouriteCreateDto dto)
        {
            var result = _favouriteService.Add(dto);
            return Ok(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll(string token)
        {
            var result = _favouriteService.GetAll(token);
            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _favouriteService.Delete(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdatePlayList(FavouriteUpdateDto dto)
        {
            var result = _favouriteService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getById")]
        public IActionResult GetByIdPlayList(int id,string token)
        {
            var result = _favouriteService.GetById(id,token);
            return Ok(result);
        }

        [HttpGet("getAllActive")]
        public IActionResult GetAllActive()
        {
            var result = _favouriteService.GetAllActive();
            return Ok(result);
        }
    }
}
