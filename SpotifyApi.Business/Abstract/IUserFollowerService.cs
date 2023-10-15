using SpotifyApi.Core.Result;
using SpotifyApi.Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IUserFollowerService
    {
        IDataResult<List<UserFollowerListDto>> GetFollowersByUserId(int userId);
        IDataResult<List<UserFollowerListDto>> GetUsersByFollowerId(int userId);
        IDataResult<bool> Create(UserFollowerCreateDto userFollowerCreateDto);
        IDataResult<bool> Delete(int id);
    }
}
