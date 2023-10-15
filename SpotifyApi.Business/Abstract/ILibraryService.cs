using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.LibraryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface ILibraryService
    {
        IDataResult<bool> Add(LibraryCreateDto libraryCreateDto);
        IDataResult<bool> Delete(int id);
        IDataResult<bool> Update(LibraryUpdateDto libraryUpdateDto);
        IDataResult<List<LibraryListDto>> GetAll(string token);
        IDataResult<LibraryListDto> GetById(int id, string token);
        IDataResult<List<LibraryListDto>> GetAllByUserId(string token, int userId);

    }
}
