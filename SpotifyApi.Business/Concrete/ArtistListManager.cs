using Newtonsoft.Json;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO.ArtistListDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class ArtistListManager : IArtistListService
    {
        public async Task<IDataResult<ArtistRelatedDto>> GetRelatedArtistById(string id,string token)
        {
            try
            {
                var url = $"https://api.spotify.com/v1/artists/{id}/related-artists";
                var getArtist = await GetByClient<ArtistRelatedDto>(url,token);
                return new SuccessDataResult<ArtistRelatedDto>(getArtist.Data, "Ok", Messages.success);
                
            }
            catch (Exception e)
            {

                return new ErrorDataResult<ArtistRelatedDto>(null, e.Message, Messages.unknown_err);
            }
        }


        private async Task<IDataResult<T>> GetByClient<T>(string url,string token) 
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
        { "Authorization", $"Bearer {token}" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<T>(body);

                    return new SuccessDataResult<T>(model, "", Messages.success);
                }
            }
            catch (Exception e)
            {

                return new ErrorDataResult<T>(default, e.Message, "");
            }
        }

        public async Task<IDataResult<ArtistListDto>> GetById(string id,string token)
        {
            try
            {
                var url =  $"https://api.spotify.com/v1/artists/{id}";
                var getBy =  await GetByClient<ArtistListDto>(url, token);
                if (getBy.Data == null)
                {
                    return new ErrorDataResult<ArtistListDto>(null, getBy.Message, getBy.MessageCode);
                }
                return new SuccessDataResult<ArtistListDto>(getBy.Data, "Ok", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<ArtistListDto>(null, e.Message, Messages.unknown_err);

            }
        }
    }
}
