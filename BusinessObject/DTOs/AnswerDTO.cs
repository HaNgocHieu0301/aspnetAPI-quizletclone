using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public string? Image { get; set; }
        [Required]
        public string Definition { get; set; } = null!;
        [Required]
        public int QuestionId { get; set; }
    }
}
