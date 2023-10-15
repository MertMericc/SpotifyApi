using SpotifyApi.Core.Entities;
using SpotifyApi.Entity.DTO.PlaylistTrackDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.FavouriteDtos
{
    public class FavouriteListDto : IDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public TrackDetailDto Track { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}
