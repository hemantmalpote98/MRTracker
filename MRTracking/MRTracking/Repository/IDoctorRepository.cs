using MRTracking.Models;

namespace MRTracking.Repository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<List<Doctor>> GetAllDoctorsAsync(string medicalRepresentativeId);
        Task<Doctor> GetDoctorByIdAsync(Guid doctorId);
        Task<List<Doctor>> GetDoctorByIdsAsync(List<Guid> doctorIds);
        Task<List<Doctor>> GetAvailableDoctorsForGroupAsync();
        Task<List<Doctor>> GetAvailableDoctorsForEditGroupAsync(Guid groupId);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task ActivateDoctorAsync(Guid doctorId, Guid userId);
        Task DeactivateDoctorAsync(Guid doctorId, Guid userId);
        Task DeleteDoctorAsync(Guid doctorId, Guid userId);
        Task<bool> SetGroupIdsForDoctorAsync(List<Guid> doctorIds, Guid MRGroupId);
    }
}
