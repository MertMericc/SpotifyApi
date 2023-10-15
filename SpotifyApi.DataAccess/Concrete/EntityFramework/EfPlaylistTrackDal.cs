using SpotifyApi.Core.EntityFramework;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.Context;
using SpotifyApi.Entity.Concrete;

namespace SpotifyApi.DataAccess.Concrete.EntityFramework
{
    public class EfPlaylistTrackDal:EfEntityRepositoryBase<PlaylistTrack,SpotifyApiDbContext>,IPlaylistTrackDal
    {
    }
}
