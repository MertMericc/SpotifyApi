using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.Album
{
    public class AlbumAddDto : IDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}
