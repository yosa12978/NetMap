using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;

namespace NetMap.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PostsController> _logger;
        public PostsController(ICategoryRepository categoryRepository, IPostRepository postRepository, IUserRepository userRepository, ILogger<PostsController> logger)
        {
            _categoriesRepository = categoryRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            PostViewModel posts = _postRepository.GetPosts(page);
            _logger.LogInformation("Getting recent posts");
            ViewBag.posts = posts;
            return View(posts);
        }

        [HttpGet]
        public IActionResult Search(string? query, int page = 1)
        {
            PostViewModel posts = _postRepository.Search(query, page);
            ViewBag.posts = posts;
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoriesRepository.GetCategories();
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(string title, string url, string? preview, long category)
        {
            _postRepository.CreatePost(title, url, preview, category, HttpContext.User.Identity.Name);
            return Redirect("/Account/");
        }

        [HttpGet]
        public IActionResult Redirect(long id)
        {
            Post post = _postRepository.IncreateViews(id);
            return Redirect(post.uri.ToString());
        }

        [HttpGet]
        public IActionResult Categories()
        {
            ViewBag.Categories = _categoriesRepository.GetCategories();
            return View();
        }

        [HttpGet]
        public IActionResult Category(long id, int page = 1)
        {
            Category category = _categoriesRepository.GetCategory(id);
            PostViewModel posts = _postRepository.GetCategoryPosts(id, page);
            ViewBag.Category = category;
            ViewBag.posts = posts;
            return View();
        }
    }
}