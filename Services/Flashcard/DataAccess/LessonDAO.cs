using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LessonDAO
    {
        // private static readonly ServicesFlashCardContext context = ServicesFlashCardContext.GetInstance();

        public static List<Lesson> GetLessons()
        {
            using var context = new ServicesFlashCardContext();
            return context.Lessons.Include(l => l.Questions).AsNoTracking().AsQueryable().ToList();
        }

        public static int AddLesson(Lesson lesson)
        {
            try
            {
                using var context = new ServicesFlashCardContext();
                context.Lessons.Add(lesson);
                context.SaveChanges();
                context.ChangeTracker.Clear();
                return lesson.LessonId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteLesson(Lesson lesson)
        {
            try
            {
                using var context = new ServicesFlashCardContext();
                context.Lessons.Remove(lesson);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateLesson(Lesson lesson)
        {
            try
            {
                using var context = new ServicesFlashCardContext();
                context.Update(lesson);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Lesson GetLessonById(int id)
        {
            try
            {
                using var context = new ServicesFlashCardContext();
                return context.Lessons.AsNoTracking().SingleOrDefault(c => c.LessonId == id) ?? throw new Exception("Lesson does not exist");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
