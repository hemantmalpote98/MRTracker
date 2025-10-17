using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRTracking.Models;
using System.Reflection.Emit;

namespace MRTracking.EntityConfiguration
{
    public class MedicalRepresentativeConfiguration : IEntityTypeConfiguration<MedicalRepresentative>
    {
        public void Configure(EntityTypeBuilder<MedicalRepresentative> entity)
        {
            entity.ToTable(name: "MedicalRepresentative", schema: "MRTracking");

            entity.HasKey(e => e.MedicalRepresentativeId);

            entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.MiddleName)
                  .HasMaxLength(100);

            entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Gender)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(e => e.Phone)
                  .IsRequired()
                  .HasMaxLength(15);

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Address)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Location)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.JobTitle)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Department)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.LocationAssigned)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.ReportingManager)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.HighestDegree)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.FieldOfStudy)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.InstitutionName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.DriversLicense)
                  .HasMaxLength(50);

            entity.Property(e => e.AvailabilityForTravel)
                  .IsRequired();

            //entity.OwnsMany(e => e.Certifications, a =>
            //{
            //    a.WithOwner().HasForeignKey("MedicalRepresentativeId");
            //    a.Property<int>("Id");
            //    a.HasKey("Id");
            //    a.Property(c => c).HasColumnName("Certification").IsRequired().HasMaxLength(100);
            //});

            //entity.OwnsMany(e => e.LanguagesSpoken, a =>
            //{
            //    a.WithOwner().HasForeignKey("MedicalRepresentativeId");
            //    a.Property<int>("Id");
            //    a.HasKey("Id");
            //    a.Property(c => c).HasColumnName("LanguageSpoken").IsRequired().HasMaxLength(50);
            //});
        }
    }
}
