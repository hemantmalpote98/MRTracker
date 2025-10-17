using MRTracking.Models.IdentityModel;
using System.Runtime.InteropServices;

namespace MRTracking.Repository
{
    public interface ITokenRepository
    {
        Task<string> CreateJWTToken(ApplicationUser applicationUser, List<string>? roles);
    }
}
