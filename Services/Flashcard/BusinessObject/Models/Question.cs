namespace BusinessObject.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int QuestionId { get; set; }
        public int LearningStatusId { get; set; }
        public bool IsStarred { get; set; }
        public string Term { get; set; }
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
