using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Repository.IRepository
{
    public interface IQuestionRepository
    {
        List<QuestionDTO> GetQuestions();
        int AddQuestion(AddQuestionDTO addQuestionDTO);
        List<Question> AddRangeQuestion(List<AddQuestionDTO> addQuestionDTOs);
        void UpdateQuestion(int id, EditQuestionDTO editQuestionDTO);
        void DeleteQuestion(int id);
        void DeleteRangeQuestion(List<QuestionDTO> questions);
        void UpdateRangeQuestion(List<QuestionDTO> questionDTOs);
    }
}
