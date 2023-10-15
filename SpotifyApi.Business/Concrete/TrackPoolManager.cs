using Azure.Core;
using Newtonsoft.Json;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO;
using SpotifyApi.Entity.DTO.SpotifApiDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolTrackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class TrackPoolManager : ITrackPoolService
    {
        public async Task<IDataResult<TrackPoolAlbumDto>> GetNewReleasesAlbums(string token)
        {
            try
            {
                var url = $"https://api.spotify.com/v1/browse/new-releases?country=TR&limit=50";
                var albums = await RequestSpotifyApi<TrackPoolAlbumDto>(url, token);
                if(albums.Data == null)
                {
                    return new ErrorDataResult<TrackPoolAlbumDto>(null, albums.Message, albums.MessageCode);
                }
                return new SuccessDataResult<TrackPoolAlbumDto>(albums.Data, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TrackPoolAlbumDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public async Task<IDataResult<TrackPoolTrackListDto>> GetTrackById(string trackId, string token)
        {
            try
            {
                var url = $"https://api.spotify.com/v1/tracks/{trackId}";
                var track = await RequestSpotifyApi<TrackPoolTrackListDto>(url, token);
                if(track == null)
                {
                    return new ErrorDataResult<TrackPoolTrackListDto>(null, track.Message, track.MessageCode);
                }
                return new SuccessDataResult<TrackPoolTrackListDto>(track.Data, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TrackPoolTrackListDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public async Task<IDataResult<TrackPoolTrackPagingDto>> GetTracks(string token, int pageSize, int pageNumber)
        {
            try
            {
                //var limit = pageSize < 1 ? 1 : pageSize;
                //pageNumber = pageNumber <= 1 ? 0 : pageNumber + 1;
                //var offset = pageNumber * pageSize;
                var albums = await GetNewReleasesAlbums(token);
                if(albums.Data == null)
                {
                    return new ErrorDataResult<TrackPoolTrackPagingDto>(null, albums.Message, albums.MessageCode);
                }
                var albumIds = albums.Data.Albums.Items.Select(a => a.Id);
                var url = $"https://api.spotify.com/v1/albums/4aLZc3UKcNs7uwHorE99wc/tracks?market=TR&limit=5"; //bu url kullanılmıyor aşağıdaki urlle değişiyor.
                var trackPool = new List<TrackPoolTrackListDto>();
                foreach (var albumId in albumIds)
                {
                    url = $"https://api.spotify.com/v1/albums/{albumId}/tracks?market=TR&limit=2";
                    trackPool.AddRange(RequestSpotifyApi<TrackPoolTrackDto>(url, token).Result.Data.Items);
                }

                pageNumber = pageNumber <= 1 ? 0 : pageNumber - 1;
                pageSize = pageSize < 1 ? 1 : pageSize;

                var skip = pageSize * pageNumber;
                var totalCount = trackPool.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                trackPool = trackPool.Skip(skip).Take(pageSize).ToList();

                var resultDto = new TrackPoolTrackPagingDto
                {
                    Tracks = trackPool,
                    Page = new PagingDto
                    {
                        Page = pageNumber + 1,
                        Size = pageSize,
                        TotalCount = totalCount,
                        TotalPages = totalPages
                    }
                };

                //var tracks = await RequestSpotifyApi<TrackPoolTrackDto>(url);
                return new SuccessDataResult<TrackPoolTrackPagingDto>(resultDto, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TrackPoolTrackPagingDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public async Task<IDataResult<T>> RequestSpotifyApi<T>(string url, string token)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Headers =
    {
        { "Accept", "application/json" },
                    { "Authorization", $"Bearer {token}" }
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    var trackPool = JsonConvert.DeserializeObject<T>(body);
                    return new SuccessDataResult<T>(trackPool, "Ok", Messages.success);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<T>(default, ex.Message, Messages.unknown_err);
            }
        }
    }
}
