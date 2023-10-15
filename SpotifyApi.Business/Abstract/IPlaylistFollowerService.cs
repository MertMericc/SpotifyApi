using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistFollowerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IPlaylistFollowerService
    {
        IDataResult<bool> Add(PlaylistFollowerCreateDto playlistCreateDto);
        IDataResult<bool> Delete(int id);
        IDataResult<bool> Update(PlaylistFollowerUpdateDto playlistUpdateDto);
        IDataResult<List<PlaylistFollower>> GetAll();
        IDataResult<PlaylistFollowerListDto> GetById(int id);
    }
}
