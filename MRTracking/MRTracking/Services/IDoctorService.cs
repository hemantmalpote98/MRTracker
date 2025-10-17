using MRTracking.Models;

namespace MRTracking.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId);
        Task<List<Doctor>> GetAvailableDoctorsForGroupAsync();
        Task<List<Doctor>> GetAvailableDoctorsForEditGroupAsync(Guid groupId);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task ActivateDoctorAsync(Guid doctorId, Guid userId);
        Task DeactivateDoctorAsync(Guid doctorId, Guid userId);
        Task DeleteDoctorAsync(Guid doctorId, Guid userId);
    }
}
