using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.EntityFramework;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.FavouriteDtos;
using SpotifyApi.Entity.DTO.PlaylistDtos;
using SpotifyApi.Entity.DTO.PlaylistTrackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class FavouriteManager : IFavouriteService
    {
        private readonly IFavouriteDal _favouriteDal;
        private readonly ITrackPoolService _trackPoolService;
        private readonly IUserService _userService;

        public FavouriteManager(IFavouriteDal favouriteDal, ITrackPoolService trackPoolService, IUserService userService)
        {
            _favouriteDal = favouriteDal;
            _trackPoolService = trackPoolService;
            _userService = userService;
        }

        public IDataResult<bool> Add(FavouriteCreateDto libraryCreateDto)
        {
            try
            {
                if (libraryCreateDto != null)
                {

                    var favourite = new Favourite()
                    {
                        UserId = libraryCreateDto.UserId,
                        TrackId = libraryCreateDto.TrackId,
                        CreatedDate = DateTime.Now,
                        Status = true
                    };
                    _favouriteDal.Add(favourite);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);

                }
                return new ErrorDataResult<bool>(false, "", Messages.err_null);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.err_null);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var favourite = _favouriteDal.Get(x => x.Id == id);
                if (favourite == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                favourite.Status = false;
                _favouriteDal.Update(favourite);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, "", e.Message);
            }
        }

        public IDataResult<List<FavouriteListDto>> GetAll(string token)
        {
            try
            {
                var favouriteList = _favouriteDal.GetList();
                if (favouriteList == null)
                {
                    return new ErrorDataResult<List<FavouriteListDto>>(null, "", Messages.err_null);
                }
                var favouriteListDto = new List<FavouriteListDto>();

                foreach (var favourite in favouriteList)
                {

                    var url = $"https://api.spotify.com/v1/tracks/{favourite.TrackId}?market=TR";
                    var trackId = _trackPoolService.RequestSpotifyApi<TrackDetailDto>(url, token).Result;

                    if (trackId.Data == null)
                    {
                        return new ErrorDataResult<List<FavouriteListDto>>(null, trackId.Message, trackId.MessageCode);
                    }


                    favouriteListDto.Add(new FavouriteListDto
                    {
                        Id = favourite.Id,
                        //UserId = favourite.UserId,
                        UserName = _userService.GetById(favourite.UserId).Data.Username,
                        Track = trackId.Data,
                        CreatedDate = favourite.CreatedDate,
                        ModifiedDate = favourite.ModifiedDate,
                        Status = favourite.Status
                    });
                }
                return new SuccessDataResult<List<FavouriteListDto>>(favouriteListDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<FavouriteListDto>>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<FavouriteListDto>> GetAllActive()
        {
            try
            {
                var datas = _favouriteDal.GetList(x => x.Status == true);
                if (datas.Count <= 0)
                {
                    return new ErrorDataResult<List<FavouriteListDto>>(null, "data not found", Messages.add_failed);
                }


                var listDto = new List<FavouriteListDto>();
                foreach (var item in datas)
                {
                    var userName = _userService.Get(x => x.Id == item.UserId).Data.Username;
                    listDto.Add(new FavouriteListDto()
                    {
                        Id = item.Id,
                        Status = item.Status,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,
                        //TrackId = item.TrackId,
                        UserId = userName,
                    });
                }
                return new SuccessDataResult<List<FavouriteListDto>>(listDto, Messages.success, "Ok");

            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<FavouriteListDto>>(null, e.Message, "error");

            }
        }

        public IDataResult<FavouriteListDto> GetById(int id, string token)
        {
            try
            {
                var favourite = _favouriteDal.Get(x => x.Id == id);
                if (favourite == null)
                {
                    return new ErrorDataResult<FavouriteListDto>(null, "", Messages.unknown_err);
                }



                var url = $"https://api.spotify.com/v1/tracks/{favourite.TrackId}?market=TR";
                var trackId = _trackPoolService.RequestSpotifyApi<TrackDetailDto>(url, token).Result;
                if (trackId.Data == null)
                {
                    return new ErrorDataResult<FavouriteListDto>(null, trackId.Message, trackId.MessageCode);
                }

                return new SuccessDataResult<FavouriteListDto>(new FavouriteListDto
                {
                    Id = favourite.Id,
                    //UserId = favourite.UserId,
                    Track = trackId.Data,
                    CreatedDate = favourite.CreatedDate,
                    ModifiedDate = favourite.ModifiedDate,
                    Status = favourite.Status
                });
            }
            catch (Exception e)
            {
                return new ErrorDataResult<FavouriteListDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Update(FavouriteUpdateDto libraryUpdateDto)
        {
            try
            {
                var favourite = _favouriteDal.Get(x => x.Id == libraryUpdateDto.Id);
                if (favourite == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                favourite.UserId = libraryUpdateDto.UserId;
                favourite.TrackId = libraryUpdateDto.TrackId;
                favourite.ModifiedDate = DateTime.Now;
                favourite.Status = libraryUpdateDto.Status;

                _favouriteDal.Update(favourite);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }
    } 
}
