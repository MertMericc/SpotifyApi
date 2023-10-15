using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos
{
    public class TrackPoolAlbumInformationDto : IDto
    {
        public string Album_Type { get; set; }
        public List<TrackPoolAlbumArtistDto> Artists { get; set; }
        public List<string> Available_Markets { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public List<TrackPoolAlbumImageDto> Images { get; set; }
        public string Name { get; set; }
        public string Release_Date { get; set; }
        public string Release_Date_Precision { get; set; }
        public int Total_Tracks { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
