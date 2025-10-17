namespace MRTracking.Models
{
    public class Doctor
    {
        // Personal Information
        public Guid DoctorId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Professional Information
        public string MedicalLicenseNumber { get; set; }
        public string? Specialty { get; set; }
        public string? HospitalAffiliation { get; set; }
        public string? Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string Location { get; set; }
        // This is live location
        public string CurrentLocation { get; set; }

        // Educational Background
        public string? HighestDegree { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? MedicalSchool { get; set; }
        public int? GraduationYear { get; set; }
        public bool? AvailabilityForConsultation { get; set; }
        public string? OfficeHours { get; set; }

        // Availability
        public string Availability1 { get; set; }
        public TimeOnly Availability1_StartTime { get; set; }
        public TimeOnly Availability1_EndTime { get; set; }
        public string? Availability2 { get; set; }
        public TimeOnly Availability2_StartTime { get; set; }
        public TimeOnly Availability2_EndTime { get; set; }
        public string? Availability3 { get; set; }
        public TimeOnly Availability3_StartTime { get; set; }
        public TimeOnly Availability3_EndTime { get; set; }
        public string? Availability4 { get; set; }
        public TimeOnly Availability4_StartTime { get; set; }
        public TimeOnly Availability4_EndTime { get; set; }

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
