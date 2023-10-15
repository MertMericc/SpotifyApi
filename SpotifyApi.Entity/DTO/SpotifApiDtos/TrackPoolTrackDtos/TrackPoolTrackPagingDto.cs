using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolTrackDtos
{
    public class TrackPoolTrackPagingDto : IDto
    {
        public List<TrackPoolTrackListDto> Tracks { get; set; }
        public PagingDto Page { get; set; }
    }
}
