using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Model
{
    public class RefreshTokenModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
