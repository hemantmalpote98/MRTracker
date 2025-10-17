using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;

namespace MRTracking.EntityConfiguration
{
    public class MRGroupConfiguration : IEntityTypeConfiguration<MRGroup>
    {
        public void Configure(EntityTypeBuilder<MRGroup> entity)
        {
            entity.HasKey(e => e.MRGroupId);

            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.IsActive)
                .IsRequired();

            entity.Property(e => e.CreatedOn)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.UpdatedOn)
                .IsRequired(false);

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50);

            // One-to-many relationships
            entity.HasMany(e => e.Doctors)
                .WithOne()
                .HasForeignKey(d => d.MRGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.MedicalStores)
                .WithOne()
                .HasForeignKey(ms => ms.MRGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.MedicalRepresentatives)
               .WithOne()
               .HasForeignKey(ms => ms.MRGroupId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
