using System.ComponentModel.DataAnnotations;

namespace CallServiceFlow.Dto
{
    public class TechinicalDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public short? MaxCalls { get; set; }
    }
}
