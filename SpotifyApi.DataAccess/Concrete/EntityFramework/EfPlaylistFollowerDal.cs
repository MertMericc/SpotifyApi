using SpotifyApi.Core.EntityFramework;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.Context;
using SpotifyApi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.DataAccess.Concrete.EntityFramework
{
    public class EfPlaylistFollowerDal:EfEntityRepositoryBase<PlaylistFollower,SpotifyApiDbContext>, IPlaylistFollowerDal
    {
    }
}
