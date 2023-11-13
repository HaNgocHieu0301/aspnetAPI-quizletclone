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

        public List<Lesson> GetLessons() => LessonDAO.GetLessons();

        public void UpdateLesson(int id, EditLessonDTO editLessonDTO)
        {
            Lesson lesson = LessonDAO.GetLessonById(id);
            lesson = mapper.Map(editLessonDTO, lesson);
            LessonDAO.UpdateLesson(lesson);
        }
    }
}
