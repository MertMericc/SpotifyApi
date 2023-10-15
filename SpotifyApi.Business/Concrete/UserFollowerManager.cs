using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
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
    public class UserFollowerManager : IUserFollowerService
    {
        private IUserFollowerDal _userFollowerDal;
        private IUserService _userService;

        public UserFollowerManager(IUserFollowerDal userFollowerDal, IUserService userService)
        {
            _userFollowerDal = userFollowerDal;
            _userService = userService;
        }

        public IDataResult<bool> Create(UserFollowerCreateDto userFollowerCreateDto)
        {
            try
            {
                var followerCheck = _userFollowerDal.Get(f => f.UserId == userFollowerCreateDto.UserId && f.FollowerId == userFollowerCreateDto.FollowerId);
                if (followerCheck == null || !followerCheck.Status)
                {
                    if (!followerCheck.Status)
                    {
                        _userFollowerDal.Update(new UserFollower
                        {
                            Id = followerCheck.Id,
                            UserId = followerCheck.UserId,
                            FollowerId = followerCheck.FollowerId,
                            CreatedDate = followerCheck.CreatedDate,
                            ModifiedDate = DateTime.Now,
                            Status = true
                        });
                    }
                    else
                    {
                        _userFollowerDal.Add(new UserFollower
                        {
                            UserId = userFollowerCreateDto.UserId,
                            FollowerId = userFollowerCreateDto.FollowerId,
                            CreatedDate = DateTime.Now,
                            Status = true
                        });
                    }
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "This follower has already followed this user", Messages.already_followed);
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
                var data = _userFollowerDal.Get(f => f.Id == id);
                if (data != null)
                {
                    _userFollowerDal.Update(new UserFollower
                    {
                        Id = data.Id,
                        UserId = data.UserId,
                        FollowerId = data.FollowerId,
                        CreatedDate = data.CreatedDate,
                        ModifiedDate = DateTime.Now,
                        Status = false
                    });
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "Data bulunamadı", Messages.err_null);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<UserFollowerListDto>> GetFollowersByUserId(int userId)
        {
            try
            {
                var followerList = _userFollowerDal.GetList(f => f.UserId == userId && f.Status);
                if (followerList != null && followerList.Count > 0)
                {
                    var followerListDto = new List<UserFollowerListDto>();
                    foreach (var follower in followerList)
                    {
                        followerListDto.Add(new UserFollowerListDto
                        {
                            Id = follower.Id,
                            User = _userService.GetById(follower.UserId).Data != null ? _userService.GetById(follower.UserId).Data.Username : "",
                            Follower = _userService.GetById(follower.FollowerId).Data != null ? _userService.GetById(follower.FollowerId).Data.Username : "",
                            CreatedDate = follower.CreatedDate,
                            ModifiedDate = follower.ModifiedDate,
                            Status = follower.Status
                        });
                    }
                    return new SuccessDataResult<List<UserFollowerListDto>>(followerListDto, "Ok", Messages.success);
                }
                return new ErrorDataResult<List<UserFollowerListDto>>(null, "Follower List not found", Messages.follower_list_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserFollowerListDto>>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<UserFollowerListDto>> GetUsersByFollowerId(int followerId)
        {
            try
            {
                var userList = _userFollowerDal.GetList(f => f.FollowerId == followerId && f.Status);
                if (userList != null && userList.Count > 0)
                {
                    var userListDto = new List<UserFollowerListDto>();
                    foreach (var user in userList)
                    {
                        userListDto.Add(new UserFollowerListDto
                        {
                            Id = user.Id,
                            User = _userService.GetById(user.UserId).Data != null ? _userService.GetById(user.UserId).Data.Username : "",
                            Follower = _userService.GetById(user.FollowerId).Data != null ? _userService.GetById(user.FollowerId).Data.Username : "",
                            CreatedDate = user.CreatedDate,
                            ModifiedDate = user.ModifiedDate,
                            Status = user.Status,
                        });
                    }
                    return new SuccessDataResult<List<UserFollowerListDto>>(userListDto, "Ok", Messages.success);
                }
                return new ErrorDataResult<List<UserFollowerListDto>>(null, "User List not found", Messages.fuser_list_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserFollowerListDto>>(null, ex.Message, Messages.unknown_err);
            }
        }
    }
}
