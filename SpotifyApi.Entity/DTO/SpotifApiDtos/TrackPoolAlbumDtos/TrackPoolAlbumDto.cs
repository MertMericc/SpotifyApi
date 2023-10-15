using SpotifyApi.Core.Entities;
using SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos
{
    public class TrackPoolAlbumDto : IDto
    {
        public TrackPoolAlbumListDto Albums { get; set; }
    }
}
