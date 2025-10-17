using MRTracking.Models;

namespace MRTracking.Services
{
    public interface IMedicalRepresentativeService
    {
        Task<IEnumerable<MedicalRepresentative>> GetAllRepresentativesAsync();
        Task<MedicalRepresentative> GetRepresentativeByIdAsync(string employeeId);
        Task AddRepresentativeAsync(MedicalRepresentative representative);
        Task UpdateRepresentativeAsync(MedicalRepresentative representative);
        Task DeleteRepresentativeAsync(string employeeId);
        Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForGroup();
        Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForEditGroup(Guid groupId);

    }
}
