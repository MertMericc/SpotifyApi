using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos
{
    public class GetNameDto : IDto
    {
        public string Name { get; set; }
    }
}
