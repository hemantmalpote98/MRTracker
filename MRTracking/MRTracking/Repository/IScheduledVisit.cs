using MRTracking.Models;

namespace MRTracking.Repository
{
    public interface IScheduledVisit
    {
        Task<List<Doctor>> GetDoctorsAvailableTodayAsync(DateTime dateToSchedule);
    }
}
