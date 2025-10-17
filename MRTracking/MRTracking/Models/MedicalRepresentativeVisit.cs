namespace MRTracking.Models
{
    public class MedicalRepresentativeVisit
    {
        public Guid VisitId { get; set; }
        public Guid MedicalRepresentativeId { get; set; }
        public Guid? VisitedToId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? MedicalStoreId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }
        public bool FollowUpRequired { get; set; }
        public DateTime? FollowUpDate { get; set; }
        // New field to store the current location of the visit
        public string CurrentLocation { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public Guid? LastVisitId { get; set; }
        public VisitUserTypeEnum VisitUserType { get; set; }
        public Guid? MRGroupId { get; set; }

        // Navigation Properties
        public MedicalRepresentative? MedicalRepresentative { get; set; }
        public Doctor? Doctor { get; set; }
        public MedicalStore? MedicalStore { get; set; }
        public MRGroup? MRGroup { get; set; }
    }
}
