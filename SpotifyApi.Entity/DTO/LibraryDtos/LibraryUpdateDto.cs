using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.LibraryDtos
{
    public class LibraryUpdateDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TypeId { get; set; }
        public string Type { get; set; }
    }
}
