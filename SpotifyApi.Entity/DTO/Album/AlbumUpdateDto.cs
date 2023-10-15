using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.Album
{
    public class AlbumUpdateDto : IDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
        public int? UserId { get; set; }
        public bool Status { get; set; }
    }
}
