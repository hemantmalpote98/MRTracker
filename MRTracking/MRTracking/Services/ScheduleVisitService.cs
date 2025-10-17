
using MRTracking.Models;
using MRTracking.Repository;

namespace MRTracking.Services
{
    public class ScheduleVisitService : IScheduleVisitService
    {
        private readonly IScheduleVisitRepository _scheduleVisitRepository;
        public ScheduleVisitService(IScheduleVisitRepository scheduleVisitRepository)
        {
            _scheduleVisitRepository = scheduleVisitRepository;
        }

        public async Task CreateScheduleVisit(DateTime scheduleDate)
        {
            var availableDoctors = await _scheduleVisitRepository.GetAvailableDoctorsAsync(scheduleDate);
            var availableMedicalStores = await _scheduleVisitRepository.GetAvailableMedicalStoresAsync();

            var newVisits = new List<ScheduleVisit>();

            foreach (var doctor in availableDoctors)
            {
                newVisits.Add(new ScheduleVisit
                {
                    ScheduleVisitId = Guid.NewGuid(),
                    DoctorId = doctor.DoctorId,
                    VisitedToId = doctor.DoctorId,
                    VisitUserType = VisitUserTypeEnum.Doctor,
                    VisitDate = scheduleDate,
                    MRGroupId = doctor.MRGroupId
                });
            }

            foreach (var store in availableMedicalStores)
            {
                newVisits.Add(new ScheduleVisit
                {
                    ScheduleVisitId = Guid.NewGuid(),
                    VisitedToId = store.MedicalStoreId,
                    MedicalStoreId = store.MedicalStoreId,
                    VisitUserType = VisitUserTypeEnum.MedicalStore,
                    VisitDate = scheduleDate,
                    MRGroupId = store.MRGroupId
                });
            }

            await _scheduleVisitRepository.AddScheduleVisitAsync(newVisits);
        }
    }
}
