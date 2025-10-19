namespace MRTracking.Models
{
    public class InitializationAPIModel
    {
        public List<string> RolesCreated { get; set; }
        public List<string> RolesExisted { get; set; }
        public bool AdminUserCreated { get; set; }
        public bool AdminUserExisted { get; set; }
        public List<string> Messages { get; set; }
    }
}
