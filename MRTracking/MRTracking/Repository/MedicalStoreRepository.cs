using Microsoft.EntityFrameworkCore;
using MRTracking.Data;
using MRTracking.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MRTracking.Repository
{
    public class MedicalStoreRepository : IMedicalStoreRepository
    {
        private readonly MRTrackingDBContext _context;

        public MedicalStoreRepository(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalStore>> GetAllMedicalStoresAsync()
        {
            return await _context.MedicalStores
                .Where(ms => !ms.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<MedicalStore>> GetAllMedicalStoresAsync(string medicalRepresentativeId)
        {
            var medicalRepresentativeIdGuid = new Guid(medicalRepresentativeId);
            var result = await (from medical in _context.MedicalStores
                                join groups in _context.MRGroups
                                on medical.MRGroupId equals groups.MRGroupId
                                join mr in _context.MedicalRepresentatives
                                on groups.MRGroupId equals mr.MRGroupId
                                where mr.MedicalRepresentativeId == medicalRepresentativeIdGuid
                                select medical).ToListAsync();
            return result;
        }

        public async Task<MedicalStore?> GetMedicalStoreByIdAsync(Guid medicalStoreId)
        {
            return await _context.MedicalStores.FirstOrDefaultAsync(ms => ms.MedicalStoreId == medicalStoreId && !ms.IsDeleted);
        }

        public async Task<List<MedicalStore>?> GetMedicalStoreByIdsAsync(List<Guid> medicalStoreIds)
        {
            return await _context.MedicalStores.Where(ms => medicalStoreIds.Contains(ms.MedicalStoreId)).ToListAsync();
        }

        public async Task AddMedicalStoreAsync(MedicalStore medicalStore)
        {
            medicalStore.CreatedOn = DateTime.UtcNow;
            _context.MedicalStores.Add(medicalStore);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicalStoreAsync(MedicalStore medicalStore)
        {
            var mrGroupId = await _context.MedicalStores.Where(d => d.MedicalStoreId == medicalStore.MedicalStoreId).Select(a => a.MRGroupId).FirstOrDefaultAsync();
            medicalStore.MRGroupId = mrGroupId;
            medicalStore.UpdatedOn = DateTime.UtcNow;
            _context.MedicalStores.Update(medicalStore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalStoreAsync(Guid medicalStoreId, Guid userId)
        {
            var medicalStore = await _context.MedicalStores.FindAsync(medicalStoreId);
            if (medicalStore != null)
            {
                medicalStore.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActivateMedicalStoreAsync(Guid medicalStoreId, Guid userId)
        {
            var medicalStore = await _context.MedicalStores.FindAsync(medicalStoreId);
            if (medicalStore != null)
            {
                medicalStore.IsActive = true;
                medicalStore.UpdatedOn = DateTime.UtcNow;
                medicalStore.UpdatedBy = userId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateMedicalStoreAsync(Guid medicalStoreId, Guid userId)
        {
            var medicalStore = await _context.MedicalStores.FindAsync(medicalStoreId);
            if (medicalStore != null)
            {
                medicalStore.IsActive = false;
                medicalStore.UpdatedOn = DateTime.UtcNow;
                medicalStore.UpdatedBy = userId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMedicalStoreAsync(Guid medicalStoreId)
        {
            var medicalStore = await _context.MedicalStores.FindAsync(medicalStoreId);
            if (medicalStore != null)
            {
                medicalStore.IsDeleted = true;
                medicalStore.UpdatedOn = DateTime.UtcNow;
                medicalStore.UpdatedBy = Guid.NewGuid();

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MedicalStore>> GetAvailableMedicalStoresForGroupAsync()
        {
            var availableMedicalStores = _context.MedicalStores
               .Where(ms => !_context.MRGroups
                   .Any(g => g.MedicalStores.Any(groupStore => groupStore.MedicalStoreId == ms.MedicalStoreId)))
               .Select(ms => ms)
               .ToList();
            return availableMedicalStores;
        }

        public async Task<List<MedicalStore>> GetAvailableMedicalStoresForEditGroupAsync(Guid groupId)
        {
            //var availableMedicalStores = _context.MedicalStores
            //   .Where(ms => !_context.MRGroups
            //       .Any(g => g.MedicalStores.Any(groupStore => groupStore.MedicalStoreId == ms.MedicalStoreId)))
            //   .Select(ms => ms)
            //   .ToList();

            // Get all doctors who do not belong to any group
            var availableMedicalStores = _context.MedicalStores
                .Where(d => !_context.MRGroups
                    .Any(g => g.MedicalStores.Any(groupStore => groupStore.MedicalStoreId == d.MedicalStoreId)))
                .Select(d => d.MedicalStoreId)
                .ToList();

            // Get all doctors that belong to the provided group
            var medicalStoreInGroup = _context.MRGroups
                .Where(g => g.MRGroupId == groupId)
                .SelectMany(g => g.MedicalStores.Select(dg => dg.MedicalStoreId))
                .ToList();

           

            // Combine doctors from both conditions: those in the provided group and those without any group
            var combinedMedicalStores = _context.MedicalStores
                .Where(d => medicalStoreInGroup.Contains(d.MedicalStoreId) || availableMedicalStores.Contains(d.MedicalStoreId))
                .ToList();

            return combinedMedicalStores;
        }

        public async Task<bool> SetGroupIdsForMedicalStoreAsync(List<Guid> medicalStoreIds, Guid MRGroupId)
        {
            // Find all medical store matching the list of medicalStoreIds
            var medicalStores = await _context.MedicalStores
                .Where(d => medicalStoreIds.Contains(d.MedicalStoreId))
                .ToListAsync();

            if (medicalStores == null || !medicalStores.Any())
            {
                // No medical store found with the given IDs
                return false;
            }

            // Set the GroupId for each doctor
            foreach (var medicalStore in medicalStores)
            {
                medicalStore.MRGroupId = MRGroupId;
            }

            // Save all changes in one transaction
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
