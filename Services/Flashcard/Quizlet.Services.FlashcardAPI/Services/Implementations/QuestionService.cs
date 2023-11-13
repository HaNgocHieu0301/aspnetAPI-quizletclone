using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Services.Interfaces;
using DataAccess.Mapper;
using Repository.IRepository;

namespace Quizlet.Services.FlashcardAPI.Services.Implementations;

public class QuestionService : IQuestionService
{
    private static readonly IMapper mapper = MapperConfig.Initialize();
    private IQuestionRepository _questionRepository;

    public QuestionService( IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public IEnumerable<QuestionDTO> GetQuestionsByLessonId(int lessonId)
    {
        var questionDtos = _questionRepository.GetQuestions().Where(o => o.LessonId == lessonId);
        return questionDtos;
    }

    public void UpdateRangeQuestion(List<EditQuestionDTO> questionDtOs)
    {
        List<EditQuestionDTO> questionListWithIdZeroTemp = questionDtOs.Where(o => o.QuestionId == 0).ToList();
        List<EditQuestionDTO> questionListWithIdNonZeroTemp = questionDtOs.Where(o => o.QuestionId != 0).ToList();
        List<QuestionDTO> questionListWithIdZeroDto = mapper.Map<List<QuestionDTO>>(questionListWithIdZeroTemp);
        _questionRepository.AddRangeQuestion(questionListWithIdZeroDto);
        _questionRepository.UpdateRangeQuestion(questionListWithIdNonZeroTemp);
    }

    public IEnumerable<QuestionDTO> UpdateQuestionWithAnswer(int lessonId)
    {
        return null;
    }
}