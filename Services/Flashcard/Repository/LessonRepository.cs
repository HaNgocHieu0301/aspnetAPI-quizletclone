using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess;
using DataAccess.Mapper;
using Repository.IRepository;

namespace Repository
{
    public class LessonRepository : ILessonRepository
    {
        private static readonly IMapper mapper = MapperConfig.Initialize();

        public int AddLesson(LessonDTO lessonDTO)
        {
            Lesson lesson = mapper.Map<Lesson>(lessonDTO);
            lesson.CreateAt = DateTime.Now;
            return LessonDAO.AddLesson(lesson);
        }

        public int AddLesson(AddLessonWithQuestionDTO lessonDTO)
        {
            Lesson lesson = mapper.Map<Lesson>(lessonDTO);
            lesson.CreateAt = DateTime.Now;
            return LessonDAO.AddLesson(lesson);
        }

        public void DeleteLesson(int id)
        {
            Lesson lesson = LessonDAO.GetLessonById(id);
            LessonDAO.DeleteLesson(lesson);
        }

        public List<LessonDTO> GetLessons()
        {
            return mapper.Map<List<LessonDTO>>(LessonDAO.GetLessons());
        }

        public bool UpdateLesson(EditLessonDTO editLessonDTO)
        {
            Lesson lesson = LessonDAO.GetLessonById(editLessonDTO.LessonId);
            if (lesson != null)
            {
                lesson = mapper.Map(editLessonDTO, lesson);
                LessonDAO.UpdateLesson(lesson);
                return true;
            }

            return false;
        }
    }
}