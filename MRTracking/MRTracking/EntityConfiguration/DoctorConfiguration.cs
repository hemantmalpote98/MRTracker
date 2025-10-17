using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;

namespace MRTracking.EntityConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> entity)
        {
            entity.ToTable(name: "Doctor", schema: "MRTracking");

            entity.HasKey(e => e.DoctorId);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.MiddleName)
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.DateOfBirth)
                .IsRequired();

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
                .HasMaxLength(200);

            entity.Property(e => e.CurrentLocation)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.MedicalLicenseNumber)
                .IsRequired()
                .HasMaxLength(50); // Removed duplicate

            entity.Property(e => e.Specialty)
                .HasMaxLength(100); // Removed IsRequired()

            entity.Property(e => e.HospitalAffiliation)
                .HasMaxLength(100); // Removed IsRequired()

            entity.Property(e => e.Department)
                .HasMaxLength(100); // Removed IsRequired()

            entity.Property(e => e.DateOfJoining); // Removed IsRequired()

            entity.Property(e => e.HighestDegree)
                .HasMaxLength(100); // Removed IsRequired()

            entity.Property(e => e.FieldOfStudy)
                .HasMaxLength(100); // Removed IsRequired()

            entity.Property(e => e.MedicalSchool)
                .HasMaxLength(100); // Removed IsRequired()

            entity.Property(e => e.GraduationYear);

            entity.Property(e => e.AvailabilityForConsultation);

            entity.Property(e => e.OfficeHours)
                .HasMaxLength(100);

            entity.Property(e => e.Availability1)
                .HasMaxLength(100);

            entity.Property(e => e.Availability1_StartTime);

            entity.Property(e => e.Availability1_EndTime);

            entity.Property(e => e.Availability2)
                .HasMaxLength(100);

            entity.Property(e => e.Availability2_StartTime);

            entity.Property(e => e.Availability2_EndTime);

            entity.Property(e => e.Availability3)
                .HasMaxLength(100);

            entity.Property(e => e.Availability3_StartTime);

            entity.Property(e => e.Availability3_EndTime);

            entity.Property(e => e.Availability4)
                .HasMaxLength(100);

            entity.Property(e => e.Availability4_StartTime);

            entity.Property(e => e.Availability4_EndTime);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedOn)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .IsRequired();

            entity.Property(e => e.UpdatedOn);

            entity.Property(e => e.UpdatedBy);
        }
    }
}
