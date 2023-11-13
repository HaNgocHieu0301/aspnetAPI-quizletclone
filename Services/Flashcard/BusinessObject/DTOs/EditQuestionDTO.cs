using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class EditQuestionDTO
    {
        [Required]
        public int QuestionId { get; set; }
        public int LearningStatusId { get; set; }
        [Required]
        public bool IsStarred { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public int LessonId { get; set; }
        public virtual ICollection<EditAnswerDTO> Answers { get; set; }
    }
}
