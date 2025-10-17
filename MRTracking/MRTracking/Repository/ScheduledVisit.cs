using Cronos;
using Microsoft.EntityFrameworkCore;
using MRTracking.Data;
using MRTracking.Models;

namespace MRTracking.Repository
{
    public class ScheduledVisit : IScheduledVisit
    {
        private readonly MRTrackingDBContext _context;

        public ScheduledVisit(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetDoctorsAvailableTodayAsync(DateTime dateToSchedule)
        {
            var today = DateTime.UtcNow;

            var doctors = await _context.Doctors
                .Where(d =>
                    IsDoctorAvailableOnScheduleDate(d.Availability1, dateToSchedule) ||
                    IsDoctorAvailableOnScheduleDate(d.Availability2, dateToSchedule) ||
                    IsDoctorAvailableOnScheduleDate(d.Availability3, dateToSchedule) ||
                    IsDoctorAvailableOnScheduleDate(d.Availability4, dateToSchedule))
                .ToListAsync();

            return doctors;
        }

        private bool IsDoctorAvailableOnScheduleDate(string cronExpression, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cronExpression))
                return false;

            var cron = CronExpression.Parse(cronExpression);
            var nextOccurrence = cron.GetNextOccurrence(date.Date);

            return nextOccurrence?.Date == date.Date;
        }

        //public async Task SaveDoctorsAvailableTodayAsync(List<Doctor> doctors)
        //{
        //    var today = DateTime.UtcNow.Date;

        //    // Clear old entries for today
        //    var existingEntries = _context.DoctorsAvailableToday
        //        .Where(e => e.AvailableDate == today);
        //    _context.DoctorsAvailableToday.RemoveRange(existingEntries);

        //    // Add new entries
        //    foreach (var doctor in doctors)
        //    {
        //        _context.DoctorsAvailableToday.Add(new DoctorsAvailableToday
        //        {
        //            DoctorId = doctor.DoctorId,
        //            DoctorName = doctor.FirstName + " " + doctor.LastName,
        //            AvailableDate = today
        //        });
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}
