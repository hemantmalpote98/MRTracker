namespace MRTracking.Models
{
    public class MedicalStore
    {
        // Medical Store Information
        public Guid MedicalStoreId { get; set; } 
        public string MedicalName { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string FSSAINo { get; set; }
        public string DLNo { get; set; }
        // This is live location
        public string CurrentLocation { get; set; }
        public string Location { get; set; }

        // Activation and Deletion Status
        public bool IsActive { get; set; } = true; // Default to Active
        public bool IsDeleted { get; set; } = false; // Default to Not Deleted

        // Timestamps
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }

        public Guid? MRGroupId { get; set; }
        //virtual public MRGroup? MRGroup { get; set; }
    }
}
