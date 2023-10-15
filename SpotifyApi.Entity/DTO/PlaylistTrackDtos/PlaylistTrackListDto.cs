using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistTrackDtos
{
    public class PlaylistTrackListDto
    {
        public int Id { get; set; }
        public string PlayListId { get; set; }
        public TrackDetailDto Track { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}
