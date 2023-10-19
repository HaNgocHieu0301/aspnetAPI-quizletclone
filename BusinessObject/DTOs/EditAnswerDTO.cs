using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class EditAnswerDTO
    {
        public string? Image { get; set; }
        [Required]
        public string Definition { get; set; } = null!;
    }
}
