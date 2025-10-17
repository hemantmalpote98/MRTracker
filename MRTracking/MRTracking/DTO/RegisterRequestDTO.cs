using System.ComponentModel.DataAnnotations;

namespace MRTracking.DTO
{
    public class RegisterRequestDTO
    {
        [EmailAddress]
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string[] Roles { get; set; }
    }
}
