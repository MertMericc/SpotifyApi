using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO.ArtistListDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IArtistListService
    {
        Task<IDataResult<ArtistRelatedDto>> GetRelatedArtistById(string id, string token);
        Task<IDataResult<ArtistListDto>> GetById(string id, string token);
    }
}
