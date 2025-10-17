namespace MRTracking.Services
{
    public interface IScheduleVisitService
    {
        Task CreateScheduleVisit(DateTime scheduleDate);
    }
}
