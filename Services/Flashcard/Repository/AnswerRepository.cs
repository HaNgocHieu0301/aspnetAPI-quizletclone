using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess;
using DataAccess.Mapper;
using Repository.IRepository;

namespace Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private static readonly IMapper mapper = MapperConfig.Initialize();

        public void AddAnswer(AnswerDTO AnswerDTO)
        {
            Answer Answer = mapper.Map<Answer>(AnswerDTO);
            AnswerDAO.AddAnswer(Answer);
        }

        public void AddRangeAnswer(List<AnswerDTO> answerDTOs)
        {
            List<Answer> answers = mapper.Map<List<Answer>>(answerDTOs);
            AnswerDAO.AddRangeAnswer(answers);
        }

        public void DeleteAnswer(int id)
        {
            Answer Answer = AnswerDAO.GetAnswerById(id);
            AnswerDAO.DeleteAnswer(Answer);
        }

        public void DeleteRangeAnswer(List<AnswerDTO> answerDTOs)
        {
            List<Answer> answers = mapper.Map<List<Answer>>(answerDTOs);
            AnswerDAO.DeleteRangeAnswer(answers);
        }

        public List<Answer> GetAnswers() => AnswerDAO.GetAnswers();

        public void UpdateAnswer(int id, EditAnswerDTO editAnswerDTO)
        {
            Answer Answer = AnswerDAO.GetAnswerById(id);
            Answer = mapper.Map(editAnswerDTO, Answer);
            AnswerDAO.UpdateAnswer(Answer);
        }

        public void UpdateRangeAnswer(List<AnswerDTO> answerDTOs)
        {
            List<Answer> answers = mapper.Map<List<Answer>>(answerDTOs);
            AnswerDAO.UpdateRangeAnswer(answers);
        }
    }
}
