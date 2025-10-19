using System.ComponentModel.DataAnnotations;

namespace MRTracking.DTO
{
    public class ResetPasswordRequestDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Token { get; set; }

        [Required]
        [MinLength(5)]
        public required string NewPassword { get; set; }
    }
}

