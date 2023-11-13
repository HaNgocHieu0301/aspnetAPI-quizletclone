using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public string? Image { get; set; }
        public string Definition { get; set; } = null!;
        public int QuestionId { get; set; }
    }
}
