using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_Management_System.Models;
using Product_Management_System.Models.Admin;
using Product_Management_System.Services.Admin;

namespace Product_Management_System.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            string userJson = HttpContext.Session.GetString("User");
            var user = new User();
            user = JsonConvert.DeserializeObject<User>(userJson);
            var result = await _productService.cartDetails(user.User_Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int Id)
        {
            try
            {
                await _productService.removeCart(Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int Id,int quantity)
        {
            try
            {
                await _productService.changeQuantity(Id,quantity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProceedToBuy(string Ids)
        {
            try
            {
               var result =  await _productService.proceedToBuy(Ids);
                if(result)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> UserOrder()
        {
            try
            {
                string userJson = HttpContext.Session.GetString("User");
                var user = new User();
                user = JsonConvert.DeserializeObject<User>(userJson);
                var result = await _productService.GetUserOrder(user.User_Id);
                return View(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
