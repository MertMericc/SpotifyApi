using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Entities.Concrete;
using SpotifyApi.Core.Result;
using SpotifyApi.Core.Security;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimDal _userOperationClaimDal;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimDal userOperationClaimDal )
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IDataResult<bool> ChangeUserPassword(UserPasswordChangeDto userPasswordChangeDto)
        {
            try
            {
                byte[] passwordsalt, passwordhash;
                HashingHelper.CreatePasswordHash(userPasswordChangeDto.NewPassword, out passwordsalt, out passwordhash);

                var user = _userService.Get(u => u.Id == userPasswordChangeDto.UserId).Data;
                if(user != null)
                {
                    user.PasswordSalt = passwordsalt;
                    user.PasswordHash = passwordhash;
                    var result = _userService.ChangeUserPassword(user);
                    return result.Success == true ? new SuccessDataResult<bool>(true, "Ok", Messages.success) : new ErrorDataResult<bool>(false, result.Message, result.MessageCode);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            try
            {
                var claims = _userService.GetClaims(user);
                if(claims != null)
                {
                    var accessToken = _tokenHelper.CreateToken(user, claims.Data);
                    return new SuccessDataResult<AccessToken>(accessToken, "Ok", Messages.success);
                }
                return new ErrorDataResult<AccessToken>(null, claims.Message, claims.MessageCode);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AccessToken>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<string> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = _userService.Get(u => u.Email == userLoginDto.Email).Data;
                if(user == null)
                {
                    return new ErrorDataResult<string>(null, "User not found", Messages.user_not_found);
                }
                if(!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordSalt, user.PasswordHash))
                {
                    return new ErrorDataResult<string>(null, "User password is wrong", Messages.user_wrong_password);
                }
                var tokenGenerator = CreateAccessToken(user);
                return new SuccessDataResult<string>(tokenGenerator.Data.Token, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                var userCheck = UserExists(userRegisterDto.Email);
                if (userCheck.Success)
                {
                    byte[] passwordsalt, passwordhash;
                    HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordsalt, out passwordhash);
                    var user = new User
                    {
                        Name = userRegisterDto.Name,
                        Surname = userRegisterDto.Surname,
                        Email = userRegisterDto.Email,
                        Username = userRegisterDto.Username,
                        Image = userRegisterDto.Image,
                        PasswordHash = passwordhash,
                        PasswordSalt = passwordsalt
                    };
                    var result = _userService.Create(user);

                   
                    if (result.Success)
                    {
                        _userOperationClaimDal.Add(new UserOperationClaim { UserId = user.Id, OperationClaimId = (int)OperationClaims.Member });


                      

                        return new SuccessDataResult<bool>(true, "Ok", Messages.success);


                    }
                    return new ErrorDataResult<bool>(false, "Add operation failed", Messages.add_failed);
                }
                return new ErrorDataResult<bool>(false, userCheck.Message, userCheck.MessageCode);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> UserExists(string mail)
        {
            try
            {
                var user = _userService.Get(u => u.Email == mail).Data;
                if(user != null)
                {
                    return new ErrorDataResult<bool>(false, "This user is registered in the system!", Messages.already_registered);
                }
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }
    }
}
