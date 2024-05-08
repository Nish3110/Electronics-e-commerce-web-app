using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_Management_System.Models;
using Product_Management_System.Models.Admin;
using Product_Management_System.Services.Admin;

namespace Product_Management_System.Controllers
{
    [Authentication]
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _productService.GetAllProduct();
                return View(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var result = await _productService.GetProductById(Id);
                if (result != null)
                {
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
        public async Task<IActionResult> addToCart(int product_Id,int quantity)
        {
            try
            {
                Cart cart = new Cart();
                string userJson = HttpContext.Session.GetString("User");
                var user = new User();
                user = JsonConvert.DeserializeObject<User>(userJson);
                var result = await _productService.addToCart(user.User_Id, product_Id, quantity);
                if (result)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
