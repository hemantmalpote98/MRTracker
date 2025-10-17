using MRTracking.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MRTracking.EntityConfiguration;
using MRTracking.Models.IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MRTracking.Data
{
    //
    public class MRTrackingDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public MRTrackingDBContext(DbContextOptions<MRTrackingDBContext> options)
        : base(options)
        {
        }

        public DbSet<MedicalRepresentative> MedicalRepresentatives { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalRepresentativeVisit> MedicalRepresentativeVisits { get; set; }
        public DbSet<MedicalStore> MedicalStores { get; set; }
        public DbSet<MRGroup> MRGroups { get; set; }
        public DbSet<ScheduleVisit> ScheduleVisits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminId = "120bcbde-ff6e-49d5-913a-b488e2a8c860";
            var mrId = "91e08da7-5a91-4dd8-b809-1fa19007d587";
            var doctorId = "2ab83b9d-b84f-4ec3-818e-e99e066ee000";
            var medicalStoreId = "8b7fc89b-645a-44da-81d8-e2a7faf6af6a";

            var roles = new List<IdentityRole>()
            { 
                new IdentityRole{ 
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                 new IdentityRole{
                    Id = mrId,
                    ConcurrencyStamp = mrId,
                    Name = "MedicalRepresentative",
                    NormalizedName = "MedicalRepresentative"
                },
                  new IdentityRole{
                    Id = doctorId,
                    ConcurrencyStamp = doctorId,
                    Name = "Doctor",
                    NormalizedName = "Doctor"
                },
                   new IdentityRole{
                    Id = medicalStoreId,
                    ConcurrencyStamp = medicalStoreId,
                    Name = "MedicalStore",
                    NormalizedName = "MedicalStore"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.ApplyConfiguration(new MedicalRepresentativeConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalRepresentativeVisitConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalStoreConfiguration());
            modelBuilder.ApplyConfiguration(new MRGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleVisitConfiguration());
        }
    }
}
