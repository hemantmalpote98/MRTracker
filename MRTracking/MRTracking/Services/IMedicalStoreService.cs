using MRTracking.Models;

namespace MRTracking.Services
{
    public interface IMedicalStoreService
    {
        Task<IEnumerable<MedicalStore>> GetAllMedicalStoresAsync();
        Task<IEnumerable<MedicalStore>> GetAvailableMedicalStoresForGroupAsync();
        Task<IEnumerable<MedicalStore>> GetAvailableMedicalStoresForEditGroupAsync(Guid groupId);
        Task<MedicalStore> GetMedicalStoreByIdAsync(Guid medicalStoreId);
        Task AddMedicalStoreAsync(MedicalStore medicalStore);
        Task UpdateMedicalStoreAsync(MedicalStore medicalStore);
        Task DeleteMedicalStoreAsync(Guid medicalStoreId);
        Task ActivateMedicalStoreAsync(Guid doctorId, Guid userId);
        Task DeactivateMedicalStoreAsync(Guid doctorId, Guid userId);
    }
}
