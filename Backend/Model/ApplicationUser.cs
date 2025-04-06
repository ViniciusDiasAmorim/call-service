using Microsoft.AspNetCore.Identity;

namespace CallServiceFlow.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool Active { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
