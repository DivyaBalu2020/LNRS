using LNRS.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LNRS.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMongoCollection<Blog> _blogs;
        private readonly IMongoCollection<User> _users;
        private const string _collectionName = "Blogs";

        public BlogService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _blogs = database.GetCollection<Blog>(_collectionName);
            _users = database.GetCollection<User>(_collectionName);

        }        

        public long GetAllBlogCount()
        {
            long blogCount = 0;
            blogCount = _blogs.CountDocuments(new BsonDocument());
            return blogCount;
        }

        public long GetBlogCountByUser(string firstName)
        {
            long blogCount = 0;
            blogCount = _blogs.CountDocuments(bl=>bl.AuthorId.FirstName.ToLower().Contains(firstName.ToLower()));
            return blogCount;
        }        

        public List<Blog> GetAllBlogs()
        {

            List<Blog> blogs = new List<Blog>();

            List<Blog> dbBlogs = _blogs.Find(new BsonDocument()).ToList();

            if (dbBlogs != null && dbBlogs.Any())
            {
                foreach (var blog in dbBlogs)
                {
                    if (blog != null && blog.AuthorId != null)
                        blogs.Add(blog);
                }
            }
            return blogs;
        }

        public List<User> GetAllUsers()
        {

            List<User> users = new List<User>();

            List<User> dbUsers = _blogs.Find(bl => true).Project(u => u.AuthorId).ToList();

            if (dbUsers != null && dbUsers.Any())
            {
                foreach (var user in dbUsers)
                {
                    if (user != null && !string.IsNullOrEmpty(user.FirstName))
                        users.Add(user);
                }
            }

            return users;
        }

        public void CreateBlog(Blog newBlog)
        {
            _blogs.InsertOne(newBlog);
        }

        public void CreateUser(User newUser)
        {
            _users.InsertOne(newUser);
        }

        public long GetWordCountByUser(string firstName, string lastName)
        {
            long wordCount = 0;
            List<Blog> blogs;
            char[] delimiters = new char[] { ' ', '\r', '\n' };

            blogs = _blogs.Find(bl => bl.AuthorId.FirstName.ToLower() == firstName.ToLower() && bl.AuthorId.LastName.ToLower() == lastName.ToLower()).ToList();

            if (blogs != null && blogs.Any())
            {
                blogs.ForEach(bl => wordCount += bl.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length);
            }
            return wordCount;
        }

    }
}
