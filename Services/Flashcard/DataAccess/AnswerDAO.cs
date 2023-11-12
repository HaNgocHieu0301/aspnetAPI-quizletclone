using AutoMapper;
using BusinessObject.Models;
using DataAccess.Mapper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AnswerDAO
    {
        private static readonly ServicesFlashCardContext context = ServicesFlashCardContext.GetInstance();
        private static readonly IMapper mapper = Config.Initialize();
        public static List<Answer> GetAnswers() => context.Answers.AsNoTracking().ToList();

        public static void AddAnswer(Answer answer)
        {
            try
            {
                context.Answers.Add(answer);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddRangeAnswer(List<Answer> answers)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Answers.AddRange(answers);
                context.SaveChanges();
                context.ChangeTracker.Clear();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public static void DeleteAnswer(Answer answer)
        {
            try
            {
                context.Answers.Remove(answer);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteRangeAnswer(List<Answer> answers)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.RemoveRange(answers);
                context.SaveChanges();
                context.ChangeTracker.Clear();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public static void UpdateAnswer(Answer answer)
        {
            try
            {
                context.Update(answer);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateRangeAnswer(List<Answer> answers)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Answers.UpdateRange(answers);
                context.SaveChanges();
                context.ChangeTracker.Clear();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public static Answer GetAnswerById(int id)
        {
            try
            {
                return context.Answers.AsNoTracking().SingleOrDefault(c => c.AnswerId == id) ?? throw new Exception("Answer does not exist");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
