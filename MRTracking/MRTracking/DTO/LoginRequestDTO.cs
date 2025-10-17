using System.ComponentModel.DataAnnotations;

namespace MRTracking.DTO
{
    public class LoginRequestDTO
    {
        [EmailAddress]
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
