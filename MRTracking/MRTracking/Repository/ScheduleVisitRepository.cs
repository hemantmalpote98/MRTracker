
using Cronos;
using Microsoft.EntityFrameworkCore;
using MRTracking.Data;
using MRTracking.Models;

namespace MRTracking.Repository
{
    public class ScheduleVisitRepository : IScheduleVisitRepository
    {
        private readonly MRTrackingDBContext _context;

        public ScheduleVisitRepository(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAvailableDoctorsAsync(DateTime date)
        {
            var doctors = await _context.Doctors
                .Where(d => d.IsActive && !d.IsDeleted).ToListAsync();

            var availableDoctors = doctors
                .Where(d => IsDoctorAvailable(d, date)).ToList();

            return availableDoctors;
        }

        private bool IsDoctorAvailable(Doctor doctor, DateTime date)
        {
            var cronExpressions = new[]
            {
                doctor.Availability1, doctor.Availability2,
                doctor.Availability3, doctor.Availability4
            };

            foreach (var cron in cronExpressions)
            {
                if (cron != null)
                {
                    var cronExpression = CronExpression.Parse(cron);
                    var nextOccurrence = cronExpression.GetNextOccurrence(date.Date);

                    if (nextOccurrence?.Date == date.Date)
                        return true;
                    else 
                        return false;
                }
            }

            return false;
        }

        public async Task<List<MedicalStore>> GetAvailableMedicalStoresAsync()
        {
            return await _context.MedicalStores
                .Where(ms => ms.IsActive && !ms.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<ScheduleVisit>> GetScheduledVisitsForDateAsync(
            Guid medicalRepId, DateTime date)
        {
            return await _context.ScheduleVisits
                .Where(sv => sv.MedicalRepresentativeId == medicalRepId
                             && sv.VisitDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task AddScheduleVisitAsync(List<ScheduleVisit> scheduleVisits)
        {
            await _context.ScheduleVisits.AddRangeAsync(scheduleVisits);
            await _context.SaveChangesAsync();
        }
    }
}
