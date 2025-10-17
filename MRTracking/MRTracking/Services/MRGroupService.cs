using MRTracking.Models;
using MRTracking.Repository;
using System.Text.RegularExpressions;

namespace MRTracking.Services
{
    public class MRGroupService : IMRGroupService
    {
        private readonly IMRGroupRepository _groupRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicalRepresentativeRepository _mrRepository;
        private readonly IMedicalStoreRepository _medicalRepository;

        public MRGroupService(IMRGroupRepository groupRepository, IDoctorRepository doctorRepository, IMedicalRepresentativeRepository mrRepository, IMedicalStoreRepository medicalRepository)
        {
            _groupRepository = groupRepository;
            _doctorRepository = doctorRepository;
            _mrRepository = mrRepository;
            _medicalRepository = medicalRepository;
        }

        public async Task<IEnumerable<MRGroup>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<MRGroup> GetGroupByIdAsync(Guid groupId)
        {
            return await _groupRepository.GetGroupByIdAsync(groupId);
        }

        public async Task<MRGroup> AddGroupAsync(MRGroup group, List<Guid> doctorIds, List<Guid> medicalStoreIds, List<Guid> medicalRepresentativeIds)
        {
            // Fetch the actual doctor entities from the database
            var doctors = await _doctorRepository.GetDoctorByIdsAsync(doctorIds);
            if (doctors != null && doctors.Any())
            {
                group.Doctors = doctors;
            }

            // Fetch the actual medical store entities
            var medicalStores = await _medicalRepository.GetMedicalStoreByIdsAsync(medicalStoreIds);
            if (medicalStores != null && medicalStores.Any())
            {
                group.MedicalStores = medicalStores;
            }

            // Fetch the actual medical representative entities
            var medicalRepresentatives = await _mrRepository.GetRepresentativeByIdsAsync(medicalRepresentativeIds);
            if (medicalRepresentatives != null && medicalRepresentatives.Any())
            {
                group.MedicalRepresentatives = medicalRepresentatives;
            }

            var updatedGroup = await _groupRepository.AddGroupAsync(group);

            return updatedGroup;
        }

        public async Task AddGroupAsync1(MRGroup group)
        {
            //if (group.Doctors != null && group.Doctors.Any())
            //{
            //    group.Doctors = await _doctorRepository.GetDoctorByIdsAsync();
            //}
            //if (group.MedicalStores != null && group.MedicalStores.Any())
            //{
            //    group.MedicalStores = await _medicalRepository.GetMedicalStoreByIdsAsync(group.MedicalStores.Select(d => d.MedicalStoreId).ToList());
            //}
            //if (group.MedicalRepresentatives != null && group.MedicalRepresentatives.Any())
            //{
            //    group.MedicalRepresentatives = await _mrRepository.GetRepresentativeByIdsAsync(group.MedicalRepresentatives.Select(d => d.MedicalRepresentativeId).ToList());
            //}
            var updatedGroup = await _groupRepository.AddGroupAsync(group);
            await _doctorRepository.SetGroupIdsForDoctorAsync(group?.Doctors?.Select(d => d.DoctorId).ToList(), updatedGroup.MRGroupId);
            await _medicalRepository.SetGroupIdsForMedicalStoreAsync(group.MedicalStores.Select(d => d.MedicalStoreId).ToList(), updatedGroup.MRGroupId);
            await _mrRepository.SetGroupIdsForMedicalRepresentativeAsync(group.MedicalRepresentatives.Select(d => d.MedicalRepresentativeId).ToList(), updatedGroup.MRGroupId);
        }

        public async Task UpdateGroupAsync(MRGroup group)
        {
            // Fetch the actual doctor entities from the database
            var doctorIds = group.Doctors.Select(d => d.DoctorId).ToList();
            var doctors = await _doctorRepository.GetDoctorByIdsAsync(doctorIds);
            if (doctors != null && doctors.Any())
            {
                group.Doctors = doctors;
            }

            // Fetch the actual medical store entities
            var medicalStoreIds = group.MedicalStores.Select(m => m.MedicalStoreId).ToList();
            var medicalStores = await _medicalRepository.GetMedicalStoreByIdsAsync(medicalStoreIds);
            if (medicalStores != null && medicalStores.Any())
            {
                group.MedicalStores = medicalStores;
            }

            // Fetch the actual medical representative entities
            var medicalRepresentativeIds = group.MedicalRepresentatives.Select(m => m.MedicalRepresentativeId).ToList();
            var medicalRepresentatives = await _mrRepository.GetRepresentativeByIdsAsync(medicalRepresentativeIds);
            if (medicalRepresentatives != null && medicalRepresentatives.Any())
            {
                group.MedicalRepresentatives = medicalRepresentatives;
            }

            await _groupRepository.UpdateGroupAsync(group);
        }

        public async Task DeleteGroupAsync(Guid groupId)
        {
            await _groupRepository.DeleteGroupAsync(groupId);
        }

        public async Task<MRGroup> GetGroupWithDetailsAsync(Guid groupId)
        {
            return await _groupRepository.GetGroupWithDetailsAsync(groupId);
        }

        public async Task AddMedicalStoreToGroupAsync(Guid groupId, Guid medicalStoreId)
        {
            await _groupRepository.AddMedicalStoreToGroupAsync(groupId, medicalStoreId);
        }

        public async Task AddDoctorToGroupAsync(Guid groupId, Guid doctorId)
        {
            await _groupRepository.AddDoctorToGroupAsync(groupId, doctorId);
        }

        public async Task RemoveMedicalStoreFromGroupAsync(Guid groupId, Guid medicalStoreId)
        {
            await _groupRepository.RemoveMedicalStoreFromGroupAsync(groupId, medicalStoreId);
        }

        public async Task RemoveDoctorFromGroupAsync(Guid groupId, Guid doctorId)
        {
            await _groupRepository.RemoveDoctorFromGroupAsync(groupId, doctorId);
        }

        public async Task<MRGroup> AddGroupAsync(MRGroup group)
        {
            var doctorIds = group?.Doctors?.Select(d => d.DoctorId).ToList();
            group.Doctors = null;
            var medicalStoreIds = group.MedicalStores.Select(d => d.MedicalStoreId).ToList();
            group.MedicalStores = null;
            var medicalRepresentativeIds = group.MedicalRepresentatives.Select(d => d.MedicalRepresentativeId).ToList();
            group.MedicalRepresentatives = null;
            var updatedGroup = await _groupRepository.AddGroupAsync(group);
            await _doctorRepository.SetGroupIdsForDoctorAsync(doctorIds, updatedGroup.MRGroupId);
            await _medicalRepository.SetGroupIdsForMedicalStoreAsync(medicalStoreIds, updatedGroup.MRGroupId);
            await _mrRepository.SetGroupIdsForMedicalRepresentativeAsync(medicalRepresentativeIds, updatedGroup.MRGroupId);
            return updatedGroup;
        }
    }
}
