using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IPlaylistService
    {
        IDataResult<bool> Add(PlaylistCreateDto playlistCreateDto);
        IDataResult<bool> Delete(string id);
        IDataResult<bool> Update(PlaylistUpdateDto playlistUpdateDto);
        IDataResult<List<PlayListDto>> GetAll();
        IDataResult<List<PlayListDto>> GetAllActive();
        IDataResult<PlayListDto> GetById(string id);

    }
}
