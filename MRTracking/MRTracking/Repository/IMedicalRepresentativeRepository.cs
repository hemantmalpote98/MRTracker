using MRTracking.Models;

namespace MRTracking.Repository
{
    public interface IMedicalRepresentativeRepository
    {
        Task<IEnumerable<MedicalRepresentative>> GetAllRepresentativesAsync();
        Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForGroup();
        Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForEditGroup(Guid groupId);
        Task<MedicalRepresentative> GetRepresentativeByIdAsync(string employeeId);
        Task<MedicalRepresentative> GetRepresentativeByEmailIdAsync(string emailId);
        Task<List<MedicalRepresentative>?> GetRepresentativeByIdsAsync(List<Guid> medicalRepresentativeIds);
        Task AddRepresentativeAsync(MedicalRepresentative representative);
        Task UpdateRepresentativeAsync(MedicalRepresentative representative);
        Task DeleteRepresentativeAsync(string employeeId);
        Task<bool> SetGroupIdsForMedicalRepresentativeAsync(List<Guid> medicalRepresentativeIds, Guid MRGroupId);
    }
}
