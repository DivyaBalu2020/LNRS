using LNRS.Models;
using LNRS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LNRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly BlogService _blogService;
        private readonly IConfiguration _configuration;

        public BlogController(IConfiguration configuration, BlogService blogService)
        {
            _configuration = configuration;
            _blogService = blogService;
        }

        [HttpGet("login")]
        public IActionResult Login(string userName)
        {            
            if (userName == "Divya")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>() { new Claim(JwtRegisteredClaimNames.Name, userName) };
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"], 
                    audience: _configuration["JWT:ValidAudience"], 
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new 
                {
                    Token = tokenString
                });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllBlogCount")]
        public long GetAllBlogCount()
        {
            Log.Information("Get the count of all blogs");
            return _blogService.GetAllBlogCount();
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllBlogs")]
        public List<Blog> GetAllBlogs()
        {
            Log.Information("Get all blogs");
            return _blogService.GetAllBlogs();
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            Log.Information("Get all users");
            return _blogService.GetAllUsers();
        }

        [HttpPost]
        [Authorize]
        [Route("CreateBlog")]
        public HttpResponseMessage CreateBlog(Blog newBlog)
        {
            Log.Information("Create new blog");
            _blogService.CreateBlog(newBlog);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateUser")]
        public HttpResponseMessage CreateUser(User newUser)
        {
            Log.Information("Create new user");
            _blogService.CreateUser(newUser);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpGet]
        [Authorize]
        [Route("GetBlogCountByAuthor")]
        public long GetBlogCountByAuthor(string firstName)
        {
            Log.Information($"Get the count of all blogs by user: {firstName}");
            return _blogService.GetBlogCountByUser(firstName);
        }

        [HttpGet]
        [Authorize]
        [Route("GetWordCountByAuthor")]
        public long GetWordCountByAuthor(string firstName, string lastName)
        {
            Log.Information($"Get the count of all words by user: {firstName} {lastName}");
            return _blogService.GetWordCountByUser(firstName, lastName);
        }
    }
}
