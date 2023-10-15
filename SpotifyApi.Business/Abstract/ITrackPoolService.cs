using SpotifyApi.Core.Entities;
using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO.SpotifApiDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolTrackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface ITrackPoolService
    {
        Task<IDataResult<TrackPoolAlbumDto>> GetNewReleasesAlbums(string token);
        Task<IDataResult<TrackPoolTrackPagingDto>> GetTracks(string token, int pageSize, int pageNumber);
        Task<IDataResult<TrackPoolTrackListDto>> GetTrackById(string trackId, string token);
        Task<IDataResult<T>> RequestSpotifyApi<T>(string url, string token);
    }
}
