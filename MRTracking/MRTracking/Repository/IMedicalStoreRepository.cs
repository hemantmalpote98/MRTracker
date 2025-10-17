using MRTracking.Models;

namespace MRTracking.Repository
{
    public interface IMedicalStoreRepository
    {
        Task<IEnumerable<MedicalStore>> GetAllMedicalStoresAsync();
        Task<List<MedicalStore>> GetAllMedicalStoresAsync(string medicalRepresentativeId);
        Task<List<MedicalStore>> GetAvailableMedicalStoresForGroupAsync();
        Task<List<MedicalStore>> GetAvailableMedicalStoresForEditGroupAsync(Guid groupId);
        Task<MedicalStore?> GetMedicalStoreByIdAsync(Guid medicalStoreId);
        Task<List<MedicalStore>?> GetMedicalStoreByIdsAsync(List<Guid> medicalStoreIds);
        Task AddMedicalStoreAsync(MedicalStore medicalStore);
        Task UpdateMedicalStoreAsync(MedicalStore medicalStore);
        Task DeleteMedicalStoreAsync(Guid medicalStoreId);
        Task ActivateMedicalStoreAsync(Guid doctmedicalStoreIdorId, Guid userId);
        Task DeactivateMedicalStoreAsync(Guid medicalStoreId, Guid userId);
        Task<bool> SetGroupIdsForMedicalStoreAsync(List<Guid> medicalStoreIds, Guid MRGroupId);
    }
}
