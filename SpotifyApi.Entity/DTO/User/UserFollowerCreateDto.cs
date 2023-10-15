﻿using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.User
{
    public class UserFollowerCreateDto : IDto
    {
        public int UserId { get; set; }
        public int FollowerId { get; set; }
    }
}
