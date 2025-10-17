using Microsoft.AspNetCore.Identity;
using MRTracking.DTO;
using MRTracking.Models.IdentityModel;

namespace MRTracking.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Register(RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new ApplicationUser { UserName = registerRequestDTO.UserName, Email = registerRequestDTO.UserName };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles is not null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return;
                    }
                }

            }
            throw new BadHttpRequestException("Something wend wrong");
        }
    }
}
