using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public int LearningStatusId { get; set; }
        public bool IsStarred { get; set; }
        public string Term { get; set; }
        public int LessonId { get; set; }
        public virtual ICollection<AnswerDTO>? Answers { get; set; }
    }
}
