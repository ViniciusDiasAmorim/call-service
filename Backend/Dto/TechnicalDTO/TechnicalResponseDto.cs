using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto.TechnicalDTO
{
    public class TechnicalResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public short? MaxCalls { get; set; }
    }
}
