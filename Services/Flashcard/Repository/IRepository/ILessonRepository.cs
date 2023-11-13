using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Repository.IRepository
{
    public interface ILessonRepository
    {
        List<LessonDTO> GetLessons();
        int AddLesson(LessonDTO lessonDTO);
        int AddLesson(AddLessonWithQuestionDTO lessonDTO);
        void UpdateLesson(int id, EditLessonDTO editLessonDTO);
        void DeleteLesson(int id);
    }
}
