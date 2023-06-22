using LNRS.Models;

namespace LNRS.Services
{
    public interface IBlogService
    {

        long GetAllBlogCount();
        List<Blog> GetAllBlogs();
        List<User> GetAllUsers();
        void CreateBlog(Blog newBlog);
        void CreateUser(User newUser);

        long GetBlogCountByUser(string firstName);
        long GetWordCountByUser(string firstName, string lastName);
    }
}
