using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistTrackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IPlaylistTrackService
    {
        IDataResult<bool> Add(PlaylistTrackCreateDto playlistCreateDto);
        IDataResult<bool> Delete(int id);
        IDataResult<bool> Update(PlaylistTrackUpdateDto playlistUpdateDto);
        IDataResult<List<PlaylistTrackListDto>> GetAll(string token);
        IDataResult<List<PlaylistTrackListDto>> GetAllByPlaylistId(string playlistId,string token);
        IDataResult<PlaylistTrackListDto> GetById(int id,string token);
    }
}
