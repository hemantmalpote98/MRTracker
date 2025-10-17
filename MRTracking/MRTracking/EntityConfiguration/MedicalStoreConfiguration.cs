using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models;

namespace MRTracking.EntityConfiguration
{
    public class MedicalStoreConfiguration : IEntityTypeConfiguration<MedicalStore>
    {
        public void Configure(EntityTypeBuilder<MedicalStore> entity)
        {
            entity.ToTable(name: "MedicalStore", schema: "MRTracking");

            entity.HasKey(e => e.MedicalStoreId);

            entity.Property(e => e.MedicalName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Address)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.GSTIN)
                 .IsRequired()
                 .HasMaxLength(100);

            entity.Property(e => e.PAN)
                 .IsRequired()
                 .HasMaxLength(100);

            entity.Property(e => e.FSSAINo)
                 .IsRequired()
                 .HasMaxLength(100);

            entity.Property(e => e.DLNo)
                 .IsRequired()
                 .HasMaxLength(100);

            // Set default values for IsActive and IsDeleted
            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true);

            entity.Property(e => e.IsDeleted)
                  .HasDefaultValue(false);

            // Handle CreatedOn and UpdatedOn properties
            entity.Property(e => e.CreatedOn)
                  .IsRequired();

            entity.Property(e => e.UpdatedOn);

            entity.Property(e => e.CreatedBy)
                  .IsRequired();

            entity.Property(e => e.UpdatedBy);
        }
    }
}
