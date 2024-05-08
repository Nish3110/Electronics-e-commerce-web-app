using Microsoft.AspNetCore.Mvc;
using Product_Management_System.Models;
using Product_Management_System.Services;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Product_Management_System.Controllers.Admin
{
    [Admin_Authentication]
    public class AdminController : Controller
    {

        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _userService.GetAllUsers();
                foreach (var item in result)
                {
                    string base64EncodedPassword = item.Password;
                    byte[] encodedBytes = Convert.FromBase64String(base64EncodedPassword);
                    string decodedPassword = Encoding.UTF8.GetString(encodedBytes);
                    item.Password = decodedPassword;
                }
                return View(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
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
                    ViewBag.success_message = "User create successfully!";
                }
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var result = await _userService.GetUserDetailById(Id);
                if (result != null)
                {
                    string base64EncodedPassword = result.Password;
                    byte[] encodedBytes = Convert.FromBase64String(base64EncodedPassword);
                    string decodedPassword = Encoding.UTF8.GetString(encodedBytes);
                    result.Password = decodedPassword;
                    return View(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int User_Id,User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (user == null && User_Id > 0)
                {
                    user.User_Id = User_Id;
                    var result = await _userService.EditUserDetail(user);
                    if (result == false)
                    {
                        ViewBag.error_message = "Something went wrong!";
                    }
                    else
                    {
                        ViewBag.success_message = "User updated successfully!";
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        public async Task<IActionResult> Detail(int Id)
        {
            try
            {
                if (Id > 0 && Id != null)
                {
                    var result = await _userService.GetUserDetailById(Id);
                    if (result != null)
                    {
                        string base64EncodedPassword = result.Password;
                        byte[] encodedBytes = Convert.FromBase64String(base64EncodedPassword);
                        string decodedPassword = Encoding.UTF8.GetString(encodedBytes);
                        result.Password = decodedPassword;
                        return View(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return Json(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                if (Id > 0 && Id != null)
                {
                    var result = await _userService.DeleteUserDetail(Id);
                    if (result)
                    {
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                return Json(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
