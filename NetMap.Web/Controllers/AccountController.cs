using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;

namespace NetMap.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUserRepository userRepository, 
                                ICategoryRepository categoryRepository, 
                                IPostRepository postRepository, 
                                IEmailRepository emailRepository, 
                                IWebHostEnvironment appEnvironment,
                                ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
            _emailRepository = emailRepository;
            _appEnvironment = appEnvironment;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Index(string? name, int page = 1)
        {
            string username = name ?? HttpContext.User.Identity.Name;
            if (!_userRepository.isUsernameExist(username))
                return NotFound();
            User user = _userRepository.GetUser(username);
            ViewBag.posts = _postRepository.GetUserPosts(username, page);
            return View(user);
        }

        [HttpGet]
        public IActionResult Confirm(string id)
        {
            if (!_userRepository.isEmailTokenExist(id))
                return View();
            _userRepository.SetAccountValid(id);
            return Redirect("/Account/Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/Account/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if(!_userRepository.isEmailPasswordExist(email, password))
            {
                ViewBag.Error = "Email or password is incorrect.";
                return View();
            }
            _logger.LogInformation($"user: {email} has been authenticated");
            ClaimsPrincipal principal = _userRepository.Authenticate(email, password);
            if(principal == null)
            {
                ViewBag.Error = "Email or password is incorrect.1";
                return View();
            }
            await HttpContext.SignInAsync(principal);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/Account/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(string username, string email, string password, string passwordC)
        {
            if(_userRepository.isUsernameExist(username))
            {
                ViewBag.Error = "Username is already in use.";
                return View();
            }
            if (_userRepository.isEmailExist(email))
            {
                ViewBag.Error = "This email address is already in use.";
                return View();
            }
            if(password != passwordC)
            {
                ViewBag.Error = "Passwords does not match.";
                return View();
            }
            string emailToken = _userRepository.ComputeHash(email+DateTime.Now.ToString());
            _userRepository.CreateUser(username, email, password, emailToken);
            _logger.LogInformation($"Created user: {username}, {email}");

            string file_message = System.IO.File.ReadAllText(_appEnvironment.WebRootPath+"/templates/confrim.html");
            string Message = string.Format(file_message, username, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Account/Confirm/{emailToken}");
            await _emailRepository.SendEmailAsync(email, "NetMap: Email Confirmation", Message);

            return Redirect("/Account/Confirm");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"User {HttpContext.User.Identity.Name} has been logged out");
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }
    }
}