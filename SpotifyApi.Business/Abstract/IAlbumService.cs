using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IAlbumService
    {
        IDataResult<bool> Add(AlbumAddDto dto);
        IDataResult<bool> Update(AlbumUpdateDto dto);
        IDataResult<bool> Delete(int id);
        IDataResult<List<AlbumListDto>> GetList();
        IDataResult<AlbumListDto> GetById(int id);
    }
}
