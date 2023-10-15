using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistTrackDtos
{
    public class PlaylistTrackUpdateDto
    {
        public int Id { get; set; }
        public string PlayListId { get; set; }
        public string TrackId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}
