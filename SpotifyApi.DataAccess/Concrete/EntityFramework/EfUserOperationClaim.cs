using SpotifyApi.Core.Entities.Concrete;
using SpotifyApi.Core.EntityFramework;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaim : EfEntityRepositoryBase<UserOperationClaim, SpotifyApiDbContext>, IUserOperationClaimDal
    {
    }
}
