using BussinessObject.Models;
using DataAccess;
using Repository.IRepository;

namespace Repository
{
    public class BlogRepository : IBlogRepository
    {
        public void AddBlog(Blog blog) => BlogDAO.AddBlog(blog);

        public void DeleteBlog(int id) => BlogDAO.DeleteBlog(id);

        public List<Blog> GetBlogs() => BlogDAO.GetAllBlog();

        public void UpdateBlog(Blog blog) => BlogDAO.UpdateBlog(blog);
    }
}
