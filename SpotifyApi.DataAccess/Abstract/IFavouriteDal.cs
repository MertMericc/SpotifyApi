using SpotifyApi.Core.Repository;
using SpotifyApi.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.DataAccess.Abstract
{
    public interface IFavouriteDal : IEntityRepository<Favourite>
    {
    }
}
