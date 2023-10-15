using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.PlaylistDtos
{
    public class GetNameAndDescDto : IDto
    {
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
