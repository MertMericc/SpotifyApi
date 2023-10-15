using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistTrackDtos
{
    public class TrackDetailDto:IDto
    {
        public string name { get; set; }
        public int popularity { get; set; }
        private string duration;
        public string duration_ms { 
            get
            {
                return duration;
            } 
            set
            {
                TimeSpan t = TimeSpan.FromMilliseconds(Convert.ToDouble(value));
                string answer = string.Format("{0:D2}m : {1:D2}s",
                                        t.Minutes,
                                        t.Seconds);
                duration = answer;
            } 
        }
    }
}
