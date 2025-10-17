namespace MRTracking.DTO
{
    public class MedicalRepresentativeVisitDTO
    {
        public Guid VisitId { get; set; }
        public Guid MedicalRepresentativeId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }
        public bool FollowUpRequired { get; set; }
        public DateTime? FollowUpDate { get; set; }
    }
}
