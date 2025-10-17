using MRTracking.Models;
using MRTracking.Repository;

namespace MRTracking.Services
{
    public class MedicalRepresentativeVisitService : IMedicalRepresentativeVisitService
    {
        private readonly IMedicalRepresentativeVisitRepository _visitRepository;

        public MedicalRepresentativeVisitService(IMedicalRepresentativeVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<IEnumerable<MedicalRepresentativeVisit>> GetAllVisitsAsync()
        {
            return await _visitRepository.GetAllVisitsAsync();
        }

        public async Task<MedicalRepresentativeVisit> GetVisitByIdAsync(int visitId)
        {
            return await _visitRepository.GetVisitByIdAsync(visitId);
        }

        public async Task AddVisitAsync(MedicalRepresentativeVisit visit)
        {
            await _visitRepository.AddVisitAsync(visit);
        }

        public async Task UpdateVisitAsync(MedicalRepresentativeVisit visit)
        {
            await _visitRepository.UpdateVisitAsync(visit);
        }

        public async Task DeleteVisitAsync(int visitId)
        {
            await _visitRepository.DeleteVisitAsync(visitId);
        }
    }
}
