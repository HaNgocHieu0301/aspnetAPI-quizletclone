namespace BusinessObject.DTOs;

public class AddLessonWithQuestionDTO
{
    public AddLessonWithQuestionDTO()
    {
        Questions = new HashSet<QuestionDTO>();
    }
    public string Title { get; set; } = null!;
    public string Description { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public int VisibleId { get; set; }
    public int FolderId { get; set; }
    public double Rate { get; set; }
    public string UserId { get; set; }
    public ICollection<QuestionDTO> Questions { get; set; }
}