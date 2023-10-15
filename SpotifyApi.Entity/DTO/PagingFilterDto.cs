using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO
{
    public class PagingFilterDto : IDto
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
