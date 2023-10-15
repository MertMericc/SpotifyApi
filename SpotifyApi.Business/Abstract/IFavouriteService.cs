using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO.FavouriteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IFavouriteService
    {
        IDataResult<bool> Add(FavouriteCreateDto libraryCreateDto);
        IDataResult<bool> Delete(int id);
        IDataResult<bool> Update(FavouriteUpdateDto libraryUpdateDto);

        IDataResult<List<FavouriteListDto>> GetAll(string token);
        IDataResult<FavouriteListDto> GetById(int id,string token);

        IDataResult<List<FavouriteListDto>> GetAllActive();


    }
}
