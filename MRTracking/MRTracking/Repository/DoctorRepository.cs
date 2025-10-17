using MRTracking.Data;
using MRTracking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MRTracking.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MRTrackingDBContext _context;

        public DoctorRepository(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync(string medicalRepresentativeId)
        {
            var medicalRepresentativeIdGuid = new Guid(medicalRepresentativeId);
            var result = await (from doctor in _context.Doctors
                         join groups in _context.MRGroups
                         on doctor.MRGroupId equals groups.MRGroupId
                         join mr in _context.MedicalRepresentatives
                         on groups.MRGroupId equals mr.MRGroupId
                         where mr.MedicalRepresentativeId == medicalRepresentativeIdGuid
                         select doctor).ToListAsync();
            return result;
        }

        public async Task<List<Doctor>> GetDoctorByIdsAsync(List<Guid> doctorIds)
        {
            return await _context.Doctors
                .Where(d => doctorIds.Contains(d.DoctorId))
                .ToListAsync();
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            doctor.CreatedOn = DateTime.Now;
            doctor.UpdatedOn = DateTime.Now;
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            doctor.UpdatedOn = DateTime.Now;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(Guid doctorId, Guid userId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                doctor.IsDeleted = true;
                doctor.UpdatedOn = DateTime.Now;
                doctor.UpdatedBy = userId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task ActivateDoctorAsync(Guid doctorId, Guid userId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                doctor.IsActive = true;
                doctor.UpdatedOn = DateTime.Now;
                doctor.UpdatedBy = userId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateDoctorAsync(Guid doctorId, Guid userId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                doctor.IsActive = false;
                doctor.UpdatedOn = DateTime.Now;
                doctor.UpdatedBy = userId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Doctor>> GetAvailableDoctorsForGroupAsync()
        {
            var availableDoctors = _context.Doctors
                .Where(d => !_context.MRGroups
                    .Any(g => g.Doctors.Any(groupDoctor => groupDoctor.DoctorId == d.DoctorId)))
                .Select(d => d)
                .ToList();
            return availableDoctors;
        }

        public async Task<List<Doctor>> GetAvailableDoctorsForEditGroupAsync(Guid groupId)
        {
            // Get all doctors that belong to the provided group
            var doctorsInGroup = _context.MRGroups
                .Where(g => g.MRGroupId == groupId)
                .SelectMany(g => g.Doctors.Select(dg => dg.DoctorId))
                .ToList();

            // Get all doctors who do not belong to any group
            var availableDoctors = _context.Doctors
                .Where(d => !_context.MRGroups
                    .Any(g => g.Doctors.Any(groupDoctor => groupDoctor.DoctorId == d.DoctorId)))
                .Select(d => d.DoctorId)
                .ToList();

            // Combine doctors from both conditions: those in the provided group and those without any group
            var combinedDoctors = _context.Doctors
                .Where(d => doctorsInGroup.Contains(d.DoctorId) || availableDoctors.Contains(d.DoctorId))
                .ToList();

            return combinedDoctors;
        }

        public async Task<bool> SetGroupIdsForDoctorAsync(List<Guid> doctorIds, Guid MRGroupId)
        {
            // Find all doctors matching the list of doctorIds
            var doctors = await _context.Doctors
                .Where(d => doctorIds.Contains(d.DoctorId))
                .ToListAsync();

            if (doctors == null || !doctors.Any())
            {
                // No doctors found with the given IDs
                return false;
            }

            // Set the GroupId for each doctor
            foreach (var doctor in doctors)
            {
                doctor.MRGroupId = MRGroupId;
            }

            // Save all changes in one transaction
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid doctorId)
        {
            return await _context.Doctors.FindAsync(doctorId);
        }
    }
}
