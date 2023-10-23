using AutoMapper;
using BusinessObject.Models;

namespace DataAccess.Mapper
{
    public class Config : Profile
    {
        public Config()
        {
            CreateMap<Question, Question>().ReverseMap();
            CreateMap<Answer, Answer>().ReverseMap();
        }

        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Config>();
            });
            return config.CreateMapper();
        }
    }
}
