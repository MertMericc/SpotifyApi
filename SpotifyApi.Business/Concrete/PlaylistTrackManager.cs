using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistTrackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class PlaylistTrackManager : IPlaylistTrackService
    {
        private  IPlaylistTrackDal _playlistTrackDal;
        private ITrackPoolService _trackPoolService;

        public PlaylistTrackManager(IPlaylistTrackDal playlistTrackDal, ITrackPoolService trackPoolService)
        {
            _playlistTrackDal = playlistTrackDal;
            _trackPoolService = trackPoolService;
        }

        public IDataResult<bool> Add(PlaylistTrackCreateDto createDto)
        {
            try
            {
                if (createDto != null)
                {
                    var playlistTrac = new PlaylistTrack()
                    {

                        PlayListId = createDto.PlayListId,
                        CreatedDate = DateTime.Now,
                        Status = true,
                        TrackId = createDto.TrackId
                    };
                    _playlistTrackDal.Add(playlistTrac);
                    return new SuccessDataResult<bool>(true, "", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "", Messages.unknown_err);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, "", e.Message);

            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var playlistTrack = _playlistTrackDal.Get(x => x.Id == id);
                if (playlistTrack!=null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.unknown_err);

                }
                playlistTrack.Status = false;
                _playlistTrackDal.Update(playlistTrack);
                return new SuccessDataResult<bool>(true, "", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, "", e.Message);

            }
        }

        public IDataResult<List<PlaylistTrackListDto>> GetAll(string token)
        {
            try
            {
                var getPlaylistTrack = _playlistTrackDal.GetList();
                if (getPlaylistTrack.Count<=0)
                {
                    return new ErrorDataResult<List<PlaylistTrackListDto>>(null, "data not found", Messages.err_null);

                }
                var playlistTrack=new List<PlaylistTrackListDto>();
                foreach (var item in getPlaylistTrack)
                {
                    var url = $"https://api.spotify.com/v1/tracks/{item.TrackId}?market=TR";

                    playlistTrack.Add(new PlaylistTrackListDto
                    {
                        Id = item.Id,
                        Track = _trackPoolService.RequestSpotifyApi<TrackDetailDto>(url, token).Result.Data,
                        Status = item.Status,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,
                        PlayListId = item.PlayListId
                    });

                }
                return new SuccessDataResult<List<PlaylistTrackListDto>>(playlistTrack, "", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<PlaylistTrackListDto>>(null, "", e.Message);

            }
        }

        public IDataResult<List<PlaylistTrackListDto>> GetAllByPlaylistId(string playlistId,string token)
        {
            try
            {
                var getPlaylistTrack = _playlistTrackDal.GetList(p => p.PlayListId == playlistId);
                if (getPlaylistTrack.Count <= 0)
                {
                    return new ErrorDataResult<List<PlaylistTrackListDto>>(null, "data not found", Messages.err_null);

                }
                //Api bağlanacak
                var playlistTrack = new List<PlaylistTrackListDto>();
                foreach (var item in getPlaylistTrack)
                {
                    var url = $"https://api.spotify.com/v1/tracks/{item.TrackId}?market=TR";

                    playlistTrack.Add(new PlaylistTrackListDto
                    {
                        Id = item.Id,
                        Track = _trackPoolService.RequestSpotifyApi<TrackDetailDto>(url,token).Result.Data,
                        Status = item.Status,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,
                        PlayListId = item.PlayListId
                    });

                }
                return new SuccessDataResult<List<PlaylistTrackListDto>>(playlistTrack, "", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<PlaylistTrackListDto>>(null, "", e.Message);

            }
        }

        public IDataResult<PlaylistTrackListDto> GetById(int id,string token)
        {
            try
            {
                var playlistTrack = _playlistTrackDal.Get(x => x.Id == id);
                if (playlistTrack==null)
                {
                    return new ErrorDataResult<PlaylistTrackListDto>(null, "", Messages.unknown_err);
                }
                var url = $"https://api.spotify.com/v1/tracks/{playlistTrack.TrackId}?market=TR";

                return new SuccessDataResult<PlaylistTrackListDto>(new PlaylistTrackListDto
                {
                    
                    PlayListId=playlistTrack.PlayListId,
                    Status=playlistTrack.Status,
                    CreatedDate=playlistTrack.CreatedDate,
                    ModifiedDate=playlistTrack.ModifiedDate,
                    Track= _trackPoolService.RequestSpotifyApi<TrackDetailDto>(url, token).Result.Data,

                });

            }
            catch (Exception e)
            {

                return new ErrorDataResult<PlaylistTrackListDto>(null, "", e.Message);

            }
        }

        public IDataResult<bool> Update(PlaylistTrackUpdateDto playlistUpdateDto)
        {
            try
            {
                var updatePlaylistTrack = _playlistTrackDal.Get(x => x.Id == playlistUpdateDto.Id);
                if (updatePlaylistTrack == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
               updatePlaylistTrack.PlayListId = playlistUpdateDto.PlayListId;
                updatePlaylistTrack.Status = playlistUpdateDto.Status;
                updatePlaylistTrack.TrackId=playlistUpdateDto.TrackId;
                updatePlaylistTrack.ModifiedDate = DateTime.Now;
                


                _playlistTrackDal.Update(updatePlaylistTrack);
                return new SuccessDataResult<bool>(true, "", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, "", e.Message);

            }
        }
    }
}
