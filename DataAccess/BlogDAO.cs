using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BlogDAO
    {
        public static List<Blog> GetAllBlog()
        {
            using var context = new ServicesBlogContext();
            return context.Blogs.ToList();
        }

        public static void AddBlog(Blog blog)
        {
            try
            {
                using var context = new ServicesBlogContext();
                context.Blogs.Add(blog);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateBlog(Blog blog)
        {
            try
            {
                using var context = new ServicesBlogContext();
                var b = context.Blogs.AsNoTracking().FirstOrDefault(c => c.BlogId == blog.BlogId) ?? throw new Exception("Blog does not exist");
                context.Blogs.Update(blog);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteBlog(int id)
        {
            try
            {
                using var context = new ServicesBlogContext();
                var blog = context.Blogs.AsNoTracking().FirstOrDefault(c => c.BlogId == id) ?? throw new Exception("Blog does not exist");
                context.Blogs.Remove(blog);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
