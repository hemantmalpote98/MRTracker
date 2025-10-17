using MRTracking.Models;
using MRTracking.Repository;

namespace MRTracking.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClaimService _claimService;
        private readonly IMedicalRepresentativeService _medicalRepresentativeService;

        public DoctorService(IDoctorRepository doctorRepository, IClaimService claimService, IMedicalRepresentativeService medicalRepresentativeService)
        {
            _doctorRepository = doctorRepository;
            _claimService = claimService;
            _medicalRepresentativeService = medicalRepresentativeService;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            var userId = _claimService.GetUserId();
            if (string.IsNullOrEmpty(userId))

            {
                return await _doctorRepository.GetAllDoctorsAsync();
            }
            return await _doctorRepository.GetAllDoctorsAsync(userId);
        }

        public async Task<List<Doctor>> GetAvailableDoctorsForGroupAsync()
        {
            return await _doctorRepository.GetAvailableDoctorsForGroupAsync();
        }

        public async Task<List<Doctor>> GetAvailableDoctorsForEditGroupAsync(Guid groupId)
        {
            return await _doctorRepository.GetAvailableDoctorsForEditGroupAsync(groupId);
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid doctorId)
        {
            return await _doctorRepository.GetDoctorByIdAsync(doctorId);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            var userId = _claimService.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var medicalRepresentative = await _medicalRepresentativeService.GetRepresentativeByIdAsync(userId);
                if (medicalRepresentative is not null && medicalRepresentative.MRGroupId is not null)
                {
                    doctor.MRGroupId = medicalRepresentative.MRGroupId;
                }
            }
            await _doctorRepository.AddDoctorAsync(doctor);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await _doctorRepository.UpdateDoctorAsync(doctor);
        }

        public async Task ActivateDoctorAsync(Guid doctorId, Guid userId)
        {
            await _doctorRepository.ActivateDoctorAsync(doctorId, userId);
        }

        public async Task DeactivateDoctorAsync(Guid doctorId, Guid userId)
        {
            await _doctorRepository.DeactivateDoctorAsync(doctorId, userId);
        }

        public async Task DeleteDoctorAsync(Guid doctorId, Guid userId)
        {
            await _doctorRepository.DeleteDoctorAsync(doctorId, userId);
        }
    }
}
