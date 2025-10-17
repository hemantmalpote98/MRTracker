using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;

namespace MRTracking.EntityConfiguration
{
    public class MedicalRepresentativeVisitConfiguration : IEntityTypeConfiguration<MedicalRepresentativeVisit>
    {
        public void Configure(EntityTypeBuilder<MedicalRepresentativeVisit> entity)
        {
            entity.ToTable(name: "MedicalRepresentativeVisit", schema: "MRTracking");

            entity.HasKey(e => e.VisitId);

            entity.Property(e => e.MedicalRepresentativeId)
                  .IsRequired();

            entity.Property(e => e.DoctorId)
                  .IsRequired();

            entity.Property(e => e.VisitDate)
                  .IsRequired();

            entity.Property(e => e.Purpose)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Notes)
                  .HasMaxLength(500);

            entity.Property(e => e.FollowUpRequired)
                  .IsRequired();

            entity.Property(e => e.FollowUpDate);

            entity.Property(e => e.CurrentLocation)
                  .IsRequired(false);

            entity.HasOne(e => e.MedicalRepresentative)
                  .WithMany()
                  .HasForeignKey(e => e.MedicalRepresentativeId);

            entity.HasOne(e => e.Doctor)
                  .WithMany()
                  .HasForeignKey(e => e.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict); 


            entity.Property(e => e.VisitUserType)
              .IsRequired();

            entity.Property(e => e.VisitedToId)
                  .IsRequired(false);

            entity.Property(e => e.MedicalStoreId)
                  .IsRequired(false);

            entity.Property(e => e.LastVisitId)
                  .IsRequired(false);

            entity.Property(e => e.MRGroupId)
                  .IsRequired(false);

            entity.HasOne<MedicalStore>(e => e.MedicalStore)
                  .WithMany()
                  .HasForeignKey(e => e.MedicalStoreId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<MRGroup>(e => e.MRGroup)
                  .WithMany()
                  .HasForeignKey(e => e.MRGroupId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
