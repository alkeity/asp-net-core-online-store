using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.DTO;
using OnlineStore.Services;

namespace OnlineStore.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // show register form if not logged in
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        // process register form, login user, redirect to home
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterPost(UserDTO user)
        {
            _userService.AddUser(user); // TODO error handling
            // TODO login
            return RedirectToAction("Index", "Home");
        }

        // show login form if not logged in
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        // process login form, login user, redirect to home
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginPost(UserDTO user)
        {
            // TODO exception handling
            if (_userService.GetUser(user.Email, user.Password) == null) return View();
            // TODO login
            return RedirectToAction("Index", "Home");
        }
    }
}
