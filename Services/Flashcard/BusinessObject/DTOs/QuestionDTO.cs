using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        [Required]
        public int LearningStatusId { get; set; }
        [Required]
        public bool IsStarred { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public int LessonId { get; set; }

        public virtual ICollection<AnswerDTO>? Answers { get; set; }
    }
}
