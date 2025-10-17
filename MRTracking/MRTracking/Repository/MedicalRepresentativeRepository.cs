using MRTracking.Data;
using MRTracking.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MRTracking.Repository
{
    public class MedicalRepresentativeRepository : IMedicalRepresentativeRepository
    {
        private readonly MRTrackingDBContext _context;

        public MedicalRepresentativeRepository(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalRepresentative>> GetAllRepresentativesAsync()
        {
            return await _context.MedicalRepresentatives.ToListAsync();
        }

        public async Task<MedicalRepresentative> GetRepresentativeByIdAsync(string employeeId)
        {
            var employeeIdGuid = Guid.Parse(employeeId);
            return await _context.MedicalRepresentatives.FindAsync(employeeIdGuid);
        }

        public async Task<MedicalRepresentative> GetRepresentativeByEmailIdAsync(string emailId)
        {
            return await _context.MedicalRepresentatives.FirstOrDefaultAsync(a => a.Email == emailId);
        }

        public async Task<List<MedicalRepresentative>?> GetRepresentativeByIdsAsync(List<Guid> medicalRepresentativeIds)
        {
            return await _context.MedicalRepresentatives.Where(mr => medicalRepresentativeIds.Contains(mr.MedicalRepresentativeId)).ToListAsync();
        }

        public async Task AddRepresentativeAsync(MedicalRepresentative representative)
        {
            await _context.MedicalRepresentatives.AddAsync(representative);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRepresentativeAsync(MedicalRepresentative representative)
        {
            _context.MedicalRepresentatives.Update(representative);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRepresentativeAsync(string employeeId)
        {
            var representative = await _context.MedicalRepresentatives.FindAsync(employeeId);
            if (representative != null)
            {
                _context.MedicalRepresentatives.Remove(representative);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForGroup()
        {
            var availableDoctors = _context.MedicalRepresentatives
                .Where(d => !_context.MRGroups
                    .Any(g => g.MedicalRepresentatives.Any(groupMR => groupMR.MRGroupId == d.MedicalRepresentativeId)))
                .Select(d => d)
                .ToList();
            return availableDoctors;
        }

        public async Task<IEnumerable<MedicalRepresentative>> GetAvailableRepresentativesForEditGroup(Guid groupId)
        {
            //var availableDoctors = _context.MedicalRepresentatives
            //    .Where(d => !_context.MRGroups
            //        .Any(g => g.MedicalRepresentatives.Any(groupMR => groupMR.MRGroupId == d.MedicalRepresentativeId)))
            //    .Select(d => d)
            //    .ToList();

            // Get all mr that belong to the provided group
            var mrInGroup = _context.MRGroups
                .Where(g => g.MRGroupId == groupId)
                .SelectMany(g => g.MedicalRepresentatives.Select(dg => dg.MedicalRepresentativeId))
                .ToList();

            // Get all mr who do not belong to any group
            var availableMR = _context.MedicalRepresentatives
                .Where(d => !_context.MRGroups
                    .Any(g => g.MedicalRepresentatives.Any(groupDoctor => groupDoctor.MedicalRepresentativeId == d.MedicalRepresentativeId)))
                .Select(d => d.MedicalRepresentativeId)
                .ToList();

            // Combine doctors from both conditions: those in the provided group and those without any group
            var combinedDoctors = _context.MedicalRepresentatives
                .Where(d => mrInGroup.Contains(d.MedicalRepresentativeId) || availableMR.Contains(d.MedicalRepresentativeId))
                .ToList();

            return combinedDoctors;
        }

        public async Task<bool> SetGroupIdsForMedicalRepresentativeAsync(List<Guid> medicalRepresentativeIds, Guid MRGroupId)
        {
            // Find all medical representative matching the list of medicalRepresentativeIds
            var medicalRepresentatives = await _context.MedicalRepresentatives
                .Where(d => medicalRepresentativeIds.Contains(d.MedicalRepresentativeId))
                .ToListAsync();

            if (medicalRepresentatives == null || !medicalRepresentatives.Any())
            {
                // No medical representative found with the given IDs
                return false;
            }

            // Set the GroupId for each doctor
            foreach (var medicalRepresentative in medicalRepresentatives)
            {
                medicalRepresentative.MRGroupId = MRGroupId;
            }

            // Save all changes in one transaction
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
