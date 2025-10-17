using MRTracking.Models;
using MRTracking.Repository;
using System.Numerics;
using System.Text.RegularExpressions;

namespace MRTracking.Services
{
    public class MedicalStoreService : IMedicalStoreService
    {
        private readonly IMedicalStoreRepository _medicalStoreRepository;
        private readonly IClaimService _claimService;
        private readonly IMedicalRepresentativeService _medicalRepresentativeService;

        public MedicalStoreService(IMedicalStoreRepository medicalStoreRepository, IClaimService claimService, IMedicalRepresentativeService medicalRepresentativeService)
        {
            _medicalStoreRepository = medicalStoreRepository;
            _claimService = claimService;
            _medicalRepresentativeService = medicalRepresentativeService;
        }

        public async Task<IEnumerable<MedicalStore>> GetAllMedicalStoresAsync()
        {
            var userId = _claimService.GetUserId();
            if (string.IsNullOrEmpty(userId))

            {
                return await _medicalStoreRepository.GetAllMedicalStoresAsync();
            }
            return await _medicalStoreRepository.GetAllMedicalStoresAsync(userId);
        }

        public async Task<MedicalStore> GetMedicalStoreByIdAsync(Guid medicalStoreId)
        {
            return await _medicalStoreRepository.GetMedicalStoreByIdAsync(medicalStoreId);
        }

        public async Task AddMedicalStoreAsync(MedicalStore medicalStore)
        {
            var userId = _claimService.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var medicalRepresentative = await _medicalRepresentativeService.GetRepresentativeByIdAsync(userId);
                if (medicalRepresentative is not null && medicalRepresentative.MRGroupId is not null)
                {
                    medicalStore.MRGroupId = medicalRepresentative.MRGroupId;
                }
            }

            await _medicalStoreRepository.AddMedicalStoreAsync(medicalStore);
        }

        public async Task UpdateMedicalStoreAsync(MedicalStore medicalStore)
        {
            await _medicalStoreRepository.UpdateMedicalStoreAsync(medicalStore);
        }

        public async Task DeleteMedicalStoreAsync(Guid medicalStoreId)
        {
            await _medicalStoreRepository.DeleteMedicalStoreAsync(medicalStoreId);
        }

        public async Task ActivateMedicalStoreAsync(Guid medicalStoreId, Guid userId)
        {
            await _medicalStoreRepository.ActivateMedicalStoreAsync(medicalStoreId, userId);
        }

        public async Task DeactivateMedicalStoreAsync(Guid medicalStoreId, Guid userId)
        {
            await _medicalStoreRepository.DeactivateMedicalStoreAsync(medicalStoreId, userId);
        }

        public async Task<IEnumerable<MedicalStore>> GetAvailableMedicalStoresForGroupAsync()
        {
            return await _medicalStoreRepository.GetAvailableMedicalStoresForGroupAsync();
        }

        public async Task<IEnumerable<MedicalStore>> GetAvailableMedicalStoresForEditGroupAsync(Guid groupId)
        {
            return await _medicalStoreRepository.GetAvailableMedicalStoresForEditGroupAsync(groupId);
        }
    }
}
