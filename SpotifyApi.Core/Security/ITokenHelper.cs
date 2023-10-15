using SpotifyApi.Core.Entities.Concrete;

namespace SpotifyApi.Core.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User users, List<OperationClaim> operationClaims);
    }
}
