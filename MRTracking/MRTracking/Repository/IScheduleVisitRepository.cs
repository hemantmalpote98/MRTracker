using MRTracking.Models;

namespace MRTracking.Repository
{
    public interface IScheduleVisitRepository
    {
        Task<List<Doctor>> GetAvailableDoctorsAsync(DateTime date);
        Task<List<MedicalStore>> GetAvailableMedicalStoresAsync();
        Task<List<ScheduleVisit>> GetScheduledVisitsForDateAsync(
            Guid medicalRepId, DateTime date);
        Task AddScheduleVisitAsync(List<ScheduleVisit> scheduleVisits);
    }
}
