using AutoMapper;
using BusinessObject.Models;
using DataAccess.Mapper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class QuestionDAO
    {
        private static readonly ServicesFlashCardContext context = ServicesFlashCardContext.GetInstance();

        private static readonly IMapper mapper = Config.Initialize();

        public static List<Question> GetQuestions() => context.Questions.Include(d => d.Answers).AsNoTracking().ToList();

        public static int AddQuestion(Question question)
        {
            try
            {
                context.Questions.Add(question);
                context.SaveChanges();
                context.ChangeTracker.Clear();
                return question.QuestionId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Question> AddRangeQuestion(List<Question> questions)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Questions.AddRange(questions);
                context.SaveChanges();
                context.ChangeTracker.Clear();
                transaction.Commit();
                return questions;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public static void DeleteQuestion(Question question)
        {
            try
            {
                context.Questions.Remove(question);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteRangeQuestion(List<Question> questions)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Questions.RemoveRange(questions);
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

        public static void UpdateQuestion(Question question)
        {
            try
            {
                context.Questions.Update(question);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateRangeQuestion(List<Question> questions)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                // Detach any existing entities with the same key
                foreach (var question in questions)
                {
                    var existingEntity = context.Questions.Local.FirstOrDefault(q => q.QuestionId == question.QuestionId);
                    if (existingEntity != null)
                    {
                        context.Entry(existingEntity).State = EntityState.Detached;
                    }
                }
                context.UpdateRange(questions);
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

        public static Question GetQuestionById(int id)
        {
            try
            {
                return context.Questions.AsNoTracking().SingleOrDefault(c => c.QuestionId == id) ?? throw new Exception("Question does not exist");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
