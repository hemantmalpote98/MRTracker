using MRTracking.Data;
using MRTracking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRTracking.Repository
{
    public class MedicalRepresentativeVisitRepository : IMedicalRepresentativeVisitRepository
    {
        private readonly MRTrackingDBContext _context;

        public MedicalRepresentativeVisitRepository(MRTrackingDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalRepresentativeVisit>> GetAllVisitsAsync()
        {
            return await _context.MedicalRepresentativeVisits.ToListAsync();
        }

        public async Task<MedicalRepresentativeVisit> GetVisitByIdAsync(int visitId)
        {
            return await _context.MedicalRepresentativeVisits.FindAsync(visitId);
        }

        public async Task AddVisitAsync(MedicalRepresentativeVisit visit)
        {
            await _context.MedicalRepresentativeVisits.AddAsync(visit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitAsync(MedicalRepresentativeVisit visit)
        {
            _context.MedicalRepresentativeVisits.Update(visit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitAsync(int visitId)
        {
            var visit = await _context.MedicalRepresentativeVisits.FindAsync(visitId);
            if (visit != null)
            {
                _context.MedicalRepresentativeVisits.Remove(visit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
