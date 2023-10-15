using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Entities.Concrete;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private ILibraryService _libraryService;

        public UserManager(IUserDal userDal, ILibraryService libraryService)
        {
            _userDal = userDal;
            _libraryService = libraryService;
        }

        public IDataResult<bool> Create(User user)
        {
            try
            {
                if (user != null)
                {
                    _userDal.Add(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "Given Dto is null", Messages.err_null);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var user = _userDal.Get(u => u.Id == id);
                if (user != null)
                {
                    _userDal.Delete(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<User> Get(Expression<Func<User, bool>> filter)
        {
            try
            {
                var user = _userDal.Get(filter);
                if (user != null)
                {
                    return new SuccessDataResult<User>(user, "Ok", Messages.success);
                }
                return new ErrorDataResult<User>(null, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<UserListDto> GetById(int id)
        {
            try
            {
                var user = _userDal.Get(u => u.Id == id);
                if (user != null)
                {
                    return new SuccessDataResult<UserListDto>(new UserListDto
                    {
                        Id = user.Id,
                        FullName = user.Name + " " + user.Surname,
                        Username = user.Username,
                        Email = user.Email,
                        Image = user.Image
                    }, "Ok", Messages.success);
                }
                return new ErrorDataResult<UserListDto>(null, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UserListDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<UserListDto>> GetList(Expression<Func<User, bool>> filter = null)
        {
            try
            {
                var userList = filter != null ? _userDal.GetList(filter) : _userDal.GetList();
                if (userList != null && userList.Count > 0)
                {
                    var userDtoList = new List<UserListDto>();
                    foreach (var user in userList)
                    {
                        userDtoList.Add(new UserListDto
                        {
                            Id = user.Id,
                            FullName = user.Name + " " + user.Surname,
                            Username = user.Username,
                            Email = user.Email,
                            Image = user.Image
                        });
                        return new SuccessDataResult<List<UserListDto>>(userDtoList, "Ok", Messages.success);
                    }
                }
                return new ErrorDataResult<List<UserListDto>>(null, "User list not found", Messages.user_list_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserListDto>>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Update(UserUpdateDto userCreateDto)
        {
            try
            {
                var user = _userDal.Get(u => u.Id == userCreateDto.Id);
                if(user != null)
                {
                    _userDal.Update(new User
                    {
                        Id = user.Id,
                        Name = userCreateDto.Name,
                        Surname = userCreateDto.Surname,
                        Username = userCreateDto.Username,
                        Email = userCreateDto.Email,
                        PasswordSalt = user.PasswordSalt,
                        PasswordHash = user.PasswordHash,
                        Image = userCreateDto.Image
                    });
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> ChangeUserPassword(User user)
        {
            try
            {
                var checkUser = _userDal.Get(u => u.Id == user.Id);
                if(checkUser != null)
                {
                    _userDal.Update(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
                throw;
            }
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            try
            {
                if(user != null)
                {
                    var claims = _userDal.GetClaims(user);
                    return new ErrorDataResult<List<OperationClaim>>(claims, "Ok", Messages.success);
                }
                return new ErrorDataResult<List<OperationClaim>>(null, "Operation claims not found", Messages.operation_claims_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OperationClaim>>(null, ex.Message, Messages.unknown_err);
            }
        }


      
    }
}
