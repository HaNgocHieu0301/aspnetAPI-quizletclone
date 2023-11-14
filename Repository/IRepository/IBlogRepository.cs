using BussinessObject.Models;

namespace Repository.IRepository
{
    public interface IBlogRepository
    {
        List<Blog> GetBlogs();
        void UpdateBlog(Blog blog);
        void AddBlog(Blog blog);
        void DeleteBlog(int id);
    }
}
