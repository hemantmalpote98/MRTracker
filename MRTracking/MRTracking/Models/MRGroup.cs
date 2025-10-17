namespace MRTracking.Models
{
    public class MRGroup
    {
        public Guid MRGroupId { get; set; }
        public string GroupName { get; set; }
        public string Location { get; set; }

        // Activation and Deletion Status
        public bool IsActive { get; set; } = true; // Default to Active
        public bool IsDeleted { get; set; } = false; // Default to Not Deleted
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public ICollection<Doctor>? Doctors { get; set; }
        public ICollection<MedicalStore>? MedicalStores { get; set; }
        public ICollection<MedicalRepresentative>? MedicalRepresentatives { get; set; }


        //// Navigation properties for linking Medical Stores, Doctors, and Medical Representatives
        //public ICollection<Doctor> Doctors { get; set; }
        //public ICollection<MedicalStore> MedicalStores { get; set; }
        //public ICollection<MedicalRepresentative> MedicalRepresentatives { get; set; }
    }
}
