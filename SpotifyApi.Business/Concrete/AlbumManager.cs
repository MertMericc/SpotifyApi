using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class AlbumManager : IAlbumService
    {
        private IAlbumDal _albumDal;
        private IUserDal _userDal;

        public AlbumManager(IAlbumDal albumDal, IUserDal userDal)
        {
            _albumDal = albumDal;
            _userDal = userDal;
        }

        public IDataResult<bool> Add(AlbumAddDto dto)
        {
            try
            {
                var user = _userDal.Get(x => x.Id == dto.UserId);
                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
                }

                if (dto == null)
                {
                    return new ErrorDataResult<bool>(false, "Entered missing information", Messages.missing_information);
                }

                _albumDal.Add(new Album()
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Status = dto.Status,
                    Url = dto.Url,
                    UserId = dto.UserId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                });
                return new SuccessDataResult<bool>(true, "Album added", Messages.album_added);
            }

            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }

        }

        public IDataResult<bool> Update(AlbumUpdateDto dto)
        {
            try
            {

                var album = _albumDal.Get(x => x.Id == dto.Id);
                if (album == null)
                {
                    return new ErrorDataResult<bool>(false, "Album not found", Messages.album_not_found);
                }

                if (dto == null)
                {
                    return new ErrorDataResult<bool>(false, "Entered missing information", Messages.missing_information);
                }

                _albumDal.Update(new Album()
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Status = dto.Status,
                    Url = dto.Url,
                    UserId = dto.UserId,
                    //CreatedDate = dto.CreatedDate,
                    ModifiedDate = DateTime.Now
                });
            return new SuccessDataResult<bool>(true, "Album added", Messages.album_added);
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
                var album = _albumDal.Get(x => x.Id == id);

                if (album == null)
                {
                    return new ErrorDataResult<bool>(false, "Album not found", Messages.user_not_found);
                }

                _albumDal.Delete(album);

                return new SuccessDataResult<bool>(true, "Album deleted", Messages.album_deleted);

            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<AlbumListDto>> GetList()
        {
            try
            {
                var albumList = _albumDal.GetList();

                if (albumList.Count > 0)
                {
                    var albumListDto = new List<AlbumListDto>();

                    foreach (var item in albumList)
                    {
                        var userName = _userDal.Get(x => x.Id == item.UserId).Name;
                        albumListDto.Add(new AlbumListDto()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            UserName = userName,
                            Status = item.Status,
                            CreatedDate = item.CreatedDate,
                            ModifiedDate = item.ModifiedDate,
                            Type = item.Type,
                            Url = item.Url
                        });
                    }

                    return new SuccessDataResult<List<AlbumListDto>>(albumListDto, "success", Messages.success);
                }

                return new SuccessDataResult<List<AlbumListDto>>(new List<AlbumListDto>(), "Album list not found", Messages.album_list_not_found);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<AlbumListDto>>(null, e.Message, Messages.unknown_err);
            }

        }

        public IDataResult<AlbumListDto> GetById(int id)
        {
            try
            {
                var album = _albumDal.Get(x => x.Id == id);

                if (album == null)
                {
                    return new ErrorDataResult<AlbumListDto>(new AlbumListDto(), "Album not found", Messages.user_not_found);
                }
                var userName = _userDal.Get(x => x.Id == album.UserId).Name;

                var albumDto = new AlbumListDto()
                {
                    Id = id,
                    Name = album.Name,
                    Status = album.Status,
                    UserName = userName,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = album.ModifiedDate,
                    Type = album.Type,
                    Url = album.Url
                };

                return new SuccessDataResult<AlbumListDto>(albumDto, "success", Messages.success);

            }
            catch (Exception e)
            {
                return new ErrorDataResult<AlbumListDto>(null, e.Message, Messages.unknown_err);
            }
        }

    }
}
