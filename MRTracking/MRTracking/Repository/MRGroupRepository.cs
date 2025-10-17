using System.Text.RegularExpressions;
using System;
using MRTracking.Data;
using MRTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace MRTracking.Repository
{
    public class MRGroupRepository : IMRGroupRepository
    {
        private readonly MRTrackingDBContext _context;

        public MRGroupRepository(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MRGroup>> GetAllGroupsAsync()
        {
            return await _context.MRGroups
                .Include(g => g.Doctors)
                .Include(g => g.MedicalStores)
                .Include(g => g.MedicalRepresentatives)
                .ToListAsync();
        }

        public async Task<MRGroup> GetGroupByIdAsync(Guid groupId)
        {
            return await _context.MRGroups
                .Include(g => g.Doctors)
                .Include(g => g.MedicalStores)
                .Include(g => g.MedicalRepresentatives)
                .FirstOrDefaultAsync(g => g.MRGroupId == groupId);
        }

        public async Task<MRGroup>? AddGroupAsync(MRGroup group)
        {
            var updatedgroup = await _context.MRGroups.AddAsync(group);
            await _context.SaveChangesAsync();
            return updatedgroup.Entity;
        }

        public async Task UpdateGroupAsync(MRGroup group)
        {
            _context.MRGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(Guid groupId)
        {
            var group = await _context.MRGroups.FindAsync(groupId);
            if (group != null)
            {
                _context.MRGroups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MRGroup> GetGroupWithDetailsAsync(Guid groupId)
        {
            return await _context.MRGroups
                .Include(g => g.Doctors)
                .Include(g => g.MedicalStores)
                .Include(g => g.MedicalRepresentatives)
                .FirstOrDefaultAsync(g => g.MRGroupId == groupId);
        }

        public async Task AddMedicalStoreToGroupAsync(Guid groupId, Guid medicalStoreId)
        {
            var group = await GetGroupWithDetailsAsync(groupId);
            var medicalStore = await _context.MedicalStores.FindAsync(medicalStoreId);

            if (group != null && medicalStore != null)
            {
                group.MedicalStores.Add(medicalStore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddDoctorToGroupAsync(Guid groupId, Guid doctorId)
        {
            var group = await GetGroupWithDetailsAsync(groupId);
            var doctor = await _context.Doctors.FindAsync(doctorId);

            if (group != null && doctor != null)
            {
                group.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveMedicalStoreFromGroupAsync(Guid groupId, Guid medicalStoreId)
        {
            var group = await GetGroupWithDetailsAsync(groupId);
            var medicalStore = group?.MedicalStores.FirstOrDefault(ms => ms.MedicalStoreId == medicalStoreId);

            if (group != null && medicalStore != null)
            {
                group.MedicalStores.Remove(medicalStore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveDoctorFromGroupAsync(Guid groupId, Guid doctorId)
        {
            var group = await GetGroupWithDetailsAsync(groupId);
            var doctor = group?.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);

            if (group != null && doctor != null)
            {
                group.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
