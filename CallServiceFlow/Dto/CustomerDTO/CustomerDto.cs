using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto.CustomerDTO
{
    public class CustomerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
