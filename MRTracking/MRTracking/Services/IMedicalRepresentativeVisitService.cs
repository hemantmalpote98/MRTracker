using MRTracking.Models;

namespace MRTracking.Services
{
    public interface IMedicalRepresentativeVisitService
    {
        Task<IEnumerable<MedicalRepresentativeVisit>> GetAllVisitsAsync();
        Task<MedicalRepresentativeVisit> GetVisitByIdAsync(int visitId);
        Task AddVisitAsync(MedicalRepresentativeVisit visit);
        Task UpdateVisitAsync(MedicalRepresentativeVisit visit);
        Task DeleteVisitAsync(int visitId);
    }
}
