using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Repository.IRepository
{
    public interface IAnswerRepository
    {
        List<Answer> GetAnswers();
        void AddAnswer(AnswerDTO answerDTO);
        void AddRangeAnswer(List<AnswerDTO> answerDTOs);
        void UpdateAnswer(int id, EditAnswerDTO editAnswerDTO);
        void UpdateRangeAnswer(List<AnswerDTO> answerDTOs);
        void DeleteAnswer(int id);
        void DeleteRangeAnswer(List<AnswerDTO> answerDTOs);
    }
}
