using MRTracking.Models;
using System.Text.RegularExpressions;

namespace MRTracking.Services
{
    public interface IMRGroupService
    {
        Task<IEnumerable<MRGroup>> GetAllGroupsAsync();
        Task<MRGroup> GetGroupByIdAsync(Guid groupId);
        Task<MRGroup> AddGroupAsync(MRGroup group);
        Task<MRGroup> AddGroupAsync(MRGroup group, List<Guid> doctorIds, List<Guid> medicalStoreIds, List<Guid> medicalRepresentativeIds);
        Task UpdateGroupAsync(MRGroup group);
        Task DeleteGroupAsync(Guid groupId);

        Task<MRGroup> GetGroupWithDetailsAsync(Guid groupId);
        Task AddMedicalStoreToGroupAsync(Guid groupId, Guid medicalStoreId);
        Task AddDoctorToGroupAsync(Guid groupId, Guid doctorId);
        Task RemoveMedicalStoreFromGroupAsync(Guid groupId, Guid medicalStoreId);
        Task RemoveDoctorFromGroupAsync(Guid groupId, Guid doctorId);
    }
}
