using Microsoft.AspNetCore.Identity;

namespace CallServiceFlow.Services
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        public bool Ativo { get; set; } = true;
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
