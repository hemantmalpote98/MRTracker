namespace MRTracking.Models
{
    public class MedicalRepresentative
    {
        // Personal Information
        public Guid MedicalRepresentativeId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Professional Information
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string? EmployeeID { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Location { get; set; }
        public string? LocationAssigned { get; set; }
        public string? ReportingManager { get; set; }

        // Educational Background
        public string? HighestDegree { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? InstitutionName { get; set; }
        public int? GraduationYear { get; set; }
        //public List<string> Certifications { get; set; }


        //// Additional Information
        //public List<string> LanguagesSpoken { get; set; }
        public bool AvailabilityForTravel { get; set; }
        public string DriversLicense { get; set; }

        public Guid? MRGroupId { get; set; }
        //virtual public MRGroup? MRGroup { get; set; }
    }

}
