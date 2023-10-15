using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.ArtistListDtos
{
    public class ArtistImageDto:IDto
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }

    }
}
