using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class EditAnswerDTO
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string? Image { get; set; }
        public string Definition { get; set; } = null!;
    }
}
