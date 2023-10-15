using Newtonsoft.Json.Linq;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.EntityFramework;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.LibraryDtos;
using SpotifyApi.Entity.DTO.PlaylistDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class LibraryManager : ILibraryService
    {
        private readonly IUserService _userService;
        private readonly IPlaylistDal _playlistDal;
        private readonly ITrackPoolService _trackPoolService;
        private readonly ILibraryDal _libraryDal;

        public LibraryManager(ILibraryDal libraryDal, IUserService userService, IPlaylistDal playlistDal, ITrackPoolService trackPoolService)
        {
            _libraryDal = libraryDal;
            _userService = userService;
            _playlistDal = playlistDal;
            _trackPoolService = trackPoolService;
        }

        public IDataResult<bool> Add(LibraryCreateDto libraryCreateDto)
        {
            try
            {
                if (libraryCreateDto != null)
                {
                    var library = new Library()
                    {
                        Type = libraryCreateDto.Type,
                        TypeId = libraryCreateDto.TypeId,
                        UserId = libraryCreateDto.UserId,
                        CreatedDate = DateTime.Now
                    };
                    _libraryDal.Add(library);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "", Messages.err_null);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var library = _libraryDal.Get(x => x.Id == id);
                if (library == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                _libraryDal.Delete(library);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<List<LibraryListDto>> GetAll(string token)
        {
            try
            {
                var libraryList = _libraryDal.GetList();
                if (libraryList == null && libraryList.Count > 0)
                {
                    return new ErrorDataResult<List<LibraryListDto>>(null, "", Messages.err_null);
                }
                var libraryListDto = new List<LibraryListDto>();
                foreach (var library in libraryList)
                {
                    string name = "";
                    if(library.Type.ToLower() == "playlist")
                    {
                        name = _playlistDal.Get(p => p.Id == library.TypeId).Name;
                    }
                    else
                    {
                        var url = $"https://api.spotify.com/v1/albums/{library.TypeId}?market=TR";
                        name = _trackPoolService.RequestSpotifyApi<GetNameDto>(url, token).Result.Data.Name;
                    }
                    libraryListDto.Add(new LibraryListDto
                    {
                        Id = library.Id,
                        UserName = _userService.Get(u => u.Id == library.UserId).Data != null ? _userService.Get(u => u.Id == library.UserId).Data.Username : "",
                        Name = name,
                        Type = library.Type,
                        TypeId = library.TypeId,
                        CreatedDate = library.CreatedDate
                    });
                }
                return new SuccessDataResult<List<LibraryListDto>>(libraryListDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<LibraryListDto>>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<LibraryListDto> GetById(int id, string token)
        {
            try
            {
                var libraryList = _libraryDal.Get(x => x.Id == id);
                if (libraryList == null)
                {
                    return new ErrorDataResult<LibraryListDto>(null, "", Messages.unknown_err);
                }
                string name = "";
                if (libraryList.Type.ToLower() == "playlist")
                {
                    name = _playlistDal.Get(p => p.Id == libraryList.TypeId).Name;
                }
                else
                {
                    var url = $"https://api.spotify.com/v1/albums/{libraryList.TypeId}?market=TR";
                    name = _trackPoolService.RequestSpotifyApi<GetNameDto>(url, token).Result.Data.Name;
                }
                return new SuccessDataResult<LibraryListDto>(new LibraryListDto
                {
                    Id = libraryList.Id,
                    UserName = _userService.Get(u => u.Id == libraryList.UserId).Data != null ? _userService.Get(u => u.Id == libraryList.UserId).Data.Username : "",
                    Name = name,
                    Type = libraryList.Type,
                    TypeId = libraryList.TypeId,
                    CreatedDate = libraryList.CreatedDate
                });
            }
            catch (Exception e)
            {
                return new ErrorDataResult<LibraryListDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<LibraryListDto>> GetAllByUserId(string token, int userId)
        {
            try
            {
                var libraryList = _libraryDal.GetList(u => u.UserId == userId);
                if (libraryList == null && libraryList.Count > 0)
                {
                    return new ErrorDataResult<List<LibraryListDto>>(null, "", Messages.err_null);
                }
                var libraryListDto = new List<LibraryListDto>();
                foreach (var library in libraryList)
                {
                    string name = "";
                    if (library.Type.ToLower() == "playlist")
                    {
                        name = _playlistDal.Get(p => p.Id == library.TypeId).Name;
                    }
                    else
                    {
                        var url = $"https://api.spotify.com/v1/albums/{library.TypeId}?market=TR";
                        name = _trackPoolService.RequestSpotifyApi<GetNameDto>(url, token).Result.Data.Name;
                    }
                    libraryListDto.Add(new LibraryListDto
                    {
                        Id = library.Id,
                        UserName = _userService.Get(u => u.Id == library.UserId).Data != null ? _userService.Get(u => u.Id == library.UserId).Data.Username : "",
                        Name = name,
                        Type = library.Type,
                        TypeId = library.TypeId,
                        CreatedDate = library.CreatedDate
                    });
                }
                return new SuccessDataResult<List<LibraryListDto>>(libraryListDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<LibraryListDto>>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Update(LibraryUpdateDto libraryUpdateDto)
        {
            try
            {
                var library = _libraryDal.Get(x => x.Id == libraryUpdateDto.Id);
                if (library == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                library.UserId = libraryUpdateDto.UserId;
                library.Type = libraryUpdateDto.Type;
                library.TypeId = libraryUpdateDto.TypeId;

                _libraryDal.Update(library);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }


    }
}
