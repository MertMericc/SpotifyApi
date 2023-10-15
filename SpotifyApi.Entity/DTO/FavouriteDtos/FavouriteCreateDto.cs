﻿using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.FavouriteDtos
{
    public class FavouriteCreateDto : IDto
    {
        public int UserId { get; set; }
        public string TrackId { get; set; }
        public bool Status { get; set; }
    }
}
