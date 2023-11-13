using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;

namespace DataAccess.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LessonDTO, Lesson>().ReverseMap();
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<AnswerDTO, Answer>().ReverseMap();
            CreateMap<EditAnswerDTO, Answer>().ReverseMap();
            CreateMap<EditAnswerDTO, AnswerDTO>().ReverseMap();
            CreateMap<EditLessonDTO, Lesson>().ReverseMap();
            CreateMap<EditQuestionDTO, Question>().ReverseMap();
            CreateMap<EditQuestionDTO, QuestionDTO>().ReverseMap();
            CreateMap<AddQuestionDTO, Question>().ReverseMap();
            CreateMap<AddLessonWithQuestionDTO, Lesson>().ReverseMap();
        }

        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            return config.CreateMapper();
        }
    }
}
