namespace MRTracking.Models
{
    public class MRGroupRequest
    {
        public Guid? MRGroupId { get; set; }
        public string GroupName { get; set; }
        public string Location { get; set; }

        // Activation and Deletion Status
        public bool IsActive { get; set; } = true; // Default to Active
        public bool IsDeleted { get; set; } = false; // Default to Not Deleted

        // Navigation properties
        public ICollection<Guid>? Doctors { get; set; }
        public ICollection<Guid>? MedicalStores { get; set; }
        public ICollection<Guid>? MedicalRepresentatives { get; set; }
    }
}
