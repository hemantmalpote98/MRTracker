namespace MRTracking.Models
{
    public class ScheduleVisit
    {
        public Guid ScheduleVisitId { get; set; }
        public Guid VisitedToId { get; set; }
        public Guid? MedicalRepresentativeId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? MedicalStoreId { get; set; }
        public Guid? MRGroupId { get; set; }
        public DateTime VisitDate { get; set; }
        public VisitUserTypeEnum VisitUserType { get; set; }
        public Guid? VisitId { get; set; }
        public Guid? LastVisitId { get; set; }
        public DateTime? FollowUpDate { get; set; }

    }
}
