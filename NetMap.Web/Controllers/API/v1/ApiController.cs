using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using NetMap.Web.Dtos;
using NetMap.Web.Filters;

namespace NetMap.Web.Controllers.API.v1
{
    [Route("api/v1")]
    [ApiController]
    [TokenAuth]
    public class ApiController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<ApiController> _logger;
        private readonly IMapper _mapper;
        public ApiController(IUserRepository userRepository,
                            IPostRepository postRepository,
                            ICategoryRepository categoryRepository,
                            ITokenRepository tokenRepository,
                            ILogger<ApiController> logger,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tokenRepository = tokenRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("posts")]
        public IEnumerable<PostDto> GetPosts(string? q)
        {
            IEnumerable<Post> posts;
            if (q == null)
                posts = _postRepository.GetAll();
            else
                posts = _postRepository.SearchAllPosts(q);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        [HttpPost("posts")]
        public IActionResult CreatePost([FromBody] PostReadDto post)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            string username = _tokenRepository.GetTokenUser(HttpContext.Request.Query["token"]).username;
            _postRepository.CreatePost(post.title, post.address, post.preview, post.category, username);
            return StatusCode(201, new { status = 201, message = "Created" });
        }

        [HttpGet("categories")]
        public IEnumerable<CategoryDto> GetCategories()
        {
            IEnumerable<Category> categories = _categoryRepository.GetCategories();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        [HttpGet("categories/{id}")]
        public IEnumerable<PostDto> GetCategoryPosts(long id)
        {
            if (!_categoryRepository.isCategoryExist(id))
                HttpContext.Response.StatusCode = 404;
            IEnumerable<Post> posts = _postRepository.GetAllCategoryPosts(id);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
    }
}