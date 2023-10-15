using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos
{
    public class TrackPoolAlbumImageDto : IDto
    {
        public int Height { get; set; }
        public string Url { get; set; }
        public int width { get; set; }
    }
}
