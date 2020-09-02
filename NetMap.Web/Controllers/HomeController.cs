using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using NetMap.Web.Models;

namespace NetMap.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IUserRepository userRepository, IPostRepository postRepository, ICategoryRepository categoryRepository, ILogger<HomeController> logger)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.posts = _postRepository.GetLastPosts(5);
            return View();
        }

        [HttpGet]
        [Route("/Api/Docs")]
        public IActionResult Apidocs()
        {
            ViewBag.token = _userRepository.GetUser(HttpContext.User.Identity.Name);
            return View("Api");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
