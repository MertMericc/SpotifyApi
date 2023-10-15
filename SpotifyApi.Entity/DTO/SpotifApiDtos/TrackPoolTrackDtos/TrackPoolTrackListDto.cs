using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolTrackDtos
{
    public class TrackPoolTrackListDto : IDto
    {
        public List<TrackPoolTrackArtisDto> Artists { get; set; }
        public int Disc_Number { get; set; }
        public int Duraction_ms { get; set; }
        public bool Explicit { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public bool Is_Local { get; set; }
        public bool Is_Playable { get; set; }
        public string Name { get; set; }
        public string Preview_Url { get; set; }
        public int Track_Number { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
