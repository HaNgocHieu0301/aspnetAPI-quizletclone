using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Repository.IRepository
{
    public interface ILessonRepository
    {
        List<Lesson> GetLessons();
        int AddLesson(LessonDTO lessonDTO);
        int AddLesson(AddLessonWithQuestionDTO lessonDTO);
        bool UpdateLesson(EditLessonDTO editLessonDTO);
        void DeleteLesson(int id);
    }
}
