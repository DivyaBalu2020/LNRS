# LNRS

The below tasks have been accomplished
- ‘Blog’ fields are ‘Id’, ‘Author Id’, ‘Published On’, ‘Text’, ‘Created On’ 
- ‘User’ fields are ‘Id’, ‘First Name’, ‘Last Name’.  
- An API that can create and retrieve ‘Blogs’ and ‘Users’  
- An author would like to see a total count of all their words.
- Logging using Serilog
- JWT Authentication minimum implementation

API Endpoints
- http://localhost:22214/api/Blog/CreateBlog
- http://localhost:22214/api/Blog/CreateUser
- http://localhost:22214/api/Blog/GetAllUsers
- http://localhost:22214/api/Blog/GetAllBlogs
- http://localhost:22214/api/Blog/GetAllBlogCount
- http://localhost:22214/api/Blog/GetBlogCountByAuthor?firstName=
- http://localhost:22214/api/Blog/GetWordCountByAuthor?firstName=&lastName=
