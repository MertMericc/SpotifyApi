using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistFollowerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class PlaylistFollowerManager : IPlaylistFollowerService
    {
        private IPlaylistFollowerDal _playlistFollowerDal;

        public PlaylistFollowerManager(IPlaylistFollowerDal playlistFollowerDal)
        {
            _playlistFollowerDal = playlistFollowerDal;
        }

        public IDataResult<bool> Add(PlaylistFollowerCreateDto playlistFollowerCreateDto)
        {
            try
            {
                if (playlistFollowerCreateDto != null)
                {
                    var playListFollower = new PlaylistFollower()
                    {
                        PlaylistId = playlistFollowerCreateDto.PlaylistId,
                        FollowerId = playlistFollowerCreateDto.FollowerId,
                        CreatedDate = DateTime.Now,
                        Status = true,

                    };
                    _playlistFollowerDal.Add(playListFollower);
                    return new SuccessDataResult<bool>(true, "", Messages.success);


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
                var playListFollower = _playlistFollowerDal.Get(x => x.Id == id);
                if (playListFollower != null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                playListFollower.Status = false;
                _playlistFollowerDal.Update(playListFollower);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);


            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, "", e.Message);

            }
        }

        public IDataResult<List<PlaylistFollower>> GetAll()
        {
            try
            {
                var getPlaylists = _playlistFollowerDal.GetList();
                if (getPlaylists.Count <= 0)
                {
                    return new ErrorDataResult<List<PlaylistFollower>>(null, "playlistfollower not found", Messages.err_null);
                }
                var playlistDto = new List<PlaylistFollower>();
                foreach (var item in getPlaylists)
                {
                    playlistDto.Add(new PlaylistFollower
                    {
                        Id = item.Id,
                        Status = item.Status,
                        CreatedDate = item.CreatedDate,
                        FollowerId=item.FollowerId,
                        ModifiedDate = item.ModifiedDate,
                        PlaylistId=item.PlaylistId

                    });
                }
                return new SuccessDataResult<List<PlaylistFollower>>(playlistDto, "", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<PlaylistFollower>>(null, "", e.Message);

            }
        }

        public IDataResult<PlaylistFollowerListDto> GetById(int id)
        {
            try
            {
                var playlist = _playlistFollowerDal.Get(x => x.Id == id);
                if (playlist == null)
                {
                    return new ErrorDataResult<PlaylistFollowerListDto>(null, "", Messages.unknown_err);
                }
                return new SuccessDataResult<PlaylistFollowerListDto>(new PlaylistFollowerListDto

                
                {
                    Status = playlist.Status,
                    CreatedDate=playlist.CreatedDate,
                    FollowerId = playlist.FollowerId,
                    Id=playlist.Id,
                    ModifedDate = playlist.ModifiedDate,
                    PlaylistId = playlist.PlaylistId

                });

            }
            catch (Exception e)
            {

                return new ErrorDataResult<PlaylistFollowerListDto>(null, "", e.Message);

            }
        }

        public IDataResult<bool> Update(PlaylistFollowerUpdateDto playlistUpdateDto)
        {
            try
            {
                var updatePlaylist = _playlistFollowerDal.Get(x => x.Id == playlistUpdateDto.Id);
                if (updatePlaylist == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                 updatePlaylist.Status = playlistUpdateDto.Status;
                updatePlaylist.PlaylistId=playlistUpdateDto.PlaylistId;
                updatePlaylist.FollowerId=playlistUpdateDto.FollowerId;
                updatePlaylist.ModifiedDate = DateTime.Now;
                

                _playlistFollowerDal.Update(updatePlaylist);
                return new SuccessDataResult<bool>(true, "", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, "", e.Message);

            }
        }
    }
}
