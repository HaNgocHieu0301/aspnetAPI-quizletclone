using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Repository.IRepository
{
    public interface ILessonRepository
    {
        List<Lesson> GetLessons();
        int AddLesson(LessonDTO lessonDTO);
        void UpdateLesson(int id, EditLessonDTO editLessonDTO);
        void DeleteLesson(int id);
    }
}
