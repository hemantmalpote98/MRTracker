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

        public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            // Parse userId string to Guid
            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                throw new BadHttpRequestException("Invalid user ID format");
            }

            // Find user by ID
            var user = await _userManager.FindByIdAsync(userGuid.ToString());
            if (user == null)
            {
                throw new BadHttpRequestException("User not found");
            }

            // Verify old password
            var isValidPassword = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!isValidPassword)
            {
                return false;
            }

            // Change password
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Succeeded;
        }
    }
}
