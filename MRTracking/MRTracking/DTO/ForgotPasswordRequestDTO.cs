using System.ComponentModel.DataAnnotations;

namespace MRTracking.DTO
{
    public class ForgotPasswordRequestDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}

