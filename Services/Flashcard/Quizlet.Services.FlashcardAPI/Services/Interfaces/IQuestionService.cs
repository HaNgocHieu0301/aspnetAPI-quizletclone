using BusinessObject.DTOs;

namespace BusinessObject.Services.Interfaces;

public interface IQuestionService
{
    public IEnumerable<QuestionDTO> GetQuestionsByLessonId(int lessonId);
    void UpdateRangeQuestion(List<EditQuestionDTO> questionDtOs);
}