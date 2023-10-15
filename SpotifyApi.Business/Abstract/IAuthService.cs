using SpotifyApi.Core.Entities.Concrete;
using SpotifyApi.Core.Result;
using SpotifyApi.Core.Security;
using SpotifyApi.Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<bool> Register(UserRegisterDto userRegisterDto);
        IDataResult<string> Login(UserLoginDto userLoginDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<bool> UserExists(string mail);
        IDataResult<bool> ChangeUserPassword(UserPasswordChangeDto userPasswordChangeDto);
    }
}
