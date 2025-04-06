using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto.CustomerDTO
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
