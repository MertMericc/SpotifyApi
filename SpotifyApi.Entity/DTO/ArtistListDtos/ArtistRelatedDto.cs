using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.ArtistListDtos
{
    public class ArtistRelatedDto:IDto
    {

        public List<ArtistListDto> artists { get; set; }
    }
}
