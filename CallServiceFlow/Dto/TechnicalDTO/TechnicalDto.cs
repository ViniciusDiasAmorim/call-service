using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto.TechnicalDTO
{
    public class TechnicalDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public short? MaxCalls { get; set; }
    }
}
