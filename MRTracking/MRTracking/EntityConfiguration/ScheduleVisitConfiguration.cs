using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;

namespace MRTracking.EntityConfiguration
{
    public class ScheduleVisitConfiguration : IEntityTypeConfiguration<ScheduleVisit>
    {
        public void Configure(EntityTypeBuilder<ScheduleVisit> entity)
        {
            entity.ToTable(name: "ScheduleVisit", schema: "MRTracking");
            entity.HasKey(e => e.ScheduleVisitId);

            entity.Property(e => e.VisitDate)
                  .IsRequired();

            entity.Property(e => e.VisitUserType)
                  .IsRequired();

            entity.HasOne<MedicalRepresentative>()
                  .WithMany()
                  .HasForeignKey(e => e.MedicalRepresentativeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Doctor>()
                  .WithMany()
                  .HasForeignKey(e => e.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<MedicalStore>()
                  .WithMany()
                  .HasForeignKey(e => e.MedicalStoreId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<MRGroup>()
                  .WithMany()
                  .HasForeignKey(e => e.MRGroupId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<MedicalRepresentativeVisit>()
                  .WithMany()
                  .HasForeignKey(e => e.VisitId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<MedicalRepresentativeVisit>()
                  .WithMany()
                  .HasForeignKey(e => e.LastVisitId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
