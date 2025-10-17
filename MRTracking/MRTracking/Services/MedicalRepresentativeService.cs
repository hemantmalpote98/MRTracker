using Microsoft.EntityFrameworkCore;
using MRTracking.Models;
using MRTracking.Repository;

namespace MRTracking.Services
{
    public class MedicalRepresentativeService : IMedicalRepresentativeService
    {
        private readonly IMedicalRepresentativeRepository _repository;

        public MedicalRepresentativeService(IMedicalRepresentativeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MedicalRepresentative>> GetAllRepresentativesAsync()
        {
            return await _repository.GetAllRepresentativesAsync();
        }

        public async Task<MedicalRepresentative> GetRepresentativeByIdAsync(string employeeId)
        {
            return await _repository.GetRepresentativeByIdAsync(employeeId);
        }

        public async Task AddRepresentativeAsync(MedicalRepresentative representative)
        {
            // Add business logic if needed
            await _repository.AddRepresentativeAsync(representative);
        }

        public async Task UpdateRepresentativeAsync(MedicalRepresentative representative)
        {
            // Add business logic if needed
            await _repository.UpdateRepresentativeAsync(representative);
        }

        public async Task DeleteRepresentativeAsync(string employeeId)
        {
            // Add business logic if needed
            await _repository.DeleteRepresentativeAsync(employeeId);
        }

        public async Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForGroup()
        {
            return await _repository.GetAvailableRepresentativesForGroup();
        }

        public async Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForEditGroup(Guid groupId)
        {
            return await _repository.GetAvailableRepresentativesForEditGroup(groupId);
        }
    }
}
