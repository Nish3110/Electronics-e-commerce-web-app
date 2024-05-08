using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_Management_System.Models;
using Product_Management_System.Services;
using System;

namespace Product_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _userService.SaveUserDetail(user);
                if (result == false)
                {
                    ViewBag.error_message = "Something went wrong!";
                }
                else
                {
                    ViewBag.success_message = "Register successfully!";
                }
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            try
            {
                if (user.Email == null || user.Password == null)
                {
                    return BadRequest(ModelState);
                }
                var result = await _userService.GetUserDetail(user.Email, user.Password);
                if (result == null)
                {
                    ViewBag.error_message = "Invalid email and password!";
                }
                else
                {
                    ViewBag.success_message = "Login successfully!";
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(result));
                }
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Admin_Authentication]
        [Authentication]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("SignIn", "User");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
