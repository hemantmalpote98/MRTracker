using MRTracking.Constant;
using System.Security.Claims;

namespace MRTracking.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.FindFirst(Constants.UserId); // Replace "Id" with your constant if needed
                return userIdClaim?.Value;
            }
            return null; // Or throw an exception, or handle the case where the claim does not exist
        }
    }
}
