using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess;
using DataAccess.Mapper;
using Repository.IRepository;

namespace Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private static readonly IMapper mapper = MapperConfig.Initialize();

        public int AddQuestion(AddQuestionDTO addQuestionDTO)
        {
            Question Question = mapper.Map<Question>(addQuestionDTO);
            return QuestionDAO.AddQuestion(Question);
        }

        public List<Question> AddRangeQuestion(List<AddQuestionDTO> addQuestionDTOs)
        {
            List<Question> questions = mapper.Map<List<Question>>(addQuestionDTOs);
            return QuestionDAO.AddRangeQuestion(questions);
        }

        public void DeleteQuestion(int id)
        {
            Question Question = QuestionDAO.GetQuestionById(id);
            QuestionDAO.DeleteQuestion(Question);
        }

        public void DeleteRangeQuestion(List<QuestionDTO> questionDTOs)
        {
            List<Question> questions = mapper.Map<List<Question>>(questionDTOs);
            QuestionDAO.DeleteRangeQuestion(questions);
        }

        public List<QuestionDTO> GetQuestions()
        {
            List<Question> questions = QuestionDAO.GetQuestions();
            List<QuestionDTO> questionDTOs = mapper.Map<List<QuestionDTO>>(questions);
            return questionDTOs;
        }

        public void UpdateQuestion(int id, EditQuestionDTO editQuestionDTO)
        {
            Question Question = QuestionDAO.GetQuestionById(id);
            Question = mapper.Map(editQuestionDTO, Question);
            QuestionDAO.UpdateQuestion(Question);
        }

        public void UpdateRangeQuestion(List<QuestionDTO> questionDTOs)
        {
            List<Question> questions = mapper.Map<List<Question>>(questionDTOs);
            QuestionDAO.UpdateRangeQuestion(questions);
        }
    }
}
