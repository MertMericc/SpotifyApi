using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.User
{
    public class UserPasswordChangeDto : IDto
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
