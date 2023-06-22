using LNRS.Models;
using LNRS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Serilog;

namespace LNRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly BlogService _blogService;       

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("GetAllBlogCount")]
        public long GetAllBlogCount()
        {
            Log.Information("Get the count of all blogs");
            return _blogService.GetAllBlogCount();
        }

        [HttpGet]
        [Route("GetAllBlogs")]
        public List<Blog> GetAllBlogs()
        {
            return _blogService.GetAllBlogs();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _blogService.GetAllUsers();
        }

        [HttpPost]
        [Route("CreateBlog")]
        public HttpResponseMessage CreateBlog(Blog newBlog)
        {
            _blogService.CreateBlog(newBlog);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage CreateUser(User newUser)
        {
            _blogService.CreateUser(newUser);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("GetBlogCountByAuthor")]
        public long GetBlogCountByAuthor(string firstName)
        {
            Log.Information($"Get the count of all blogs by user: {firstName}");
            return _blogService.GetBlogCountByUser(firstName);
        }

        [HttpGet]
        [Route("GetWordCountByAuthor")]
        public long GetWordCountByAuthor(string firstName, string lastName)
        {
            Log.Information($"Get the count of all words by user: {firstName} {lastName}");
            return _blogService.GetWordCountByUser(firstName, lastName);
        }
    }
}
