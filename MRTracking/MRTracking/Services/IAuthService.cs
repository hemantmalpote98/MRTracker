using MRTracking.DTO;

namespace MRTracking.Services
{
    public interface IAuthService
    {
        Task Register(RegisterRequestDTO registerRequestDTO);
    }
}
