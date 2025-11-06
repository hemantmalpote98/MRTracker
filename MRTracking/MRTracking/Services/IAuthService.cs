using MRTracking.DTO;

namespace MRTracking.Services
{
    public interface IAuthService
    {
        Task Register(RegisterRequestDTO registerRequestDTO);
        Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
