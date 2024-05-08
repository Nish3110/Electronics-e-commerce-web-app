using Microsoft.AspNetCore.Mvc;
using Product_Management_System.Models;
using Product_Management_System.Services.Admin;

namespace Product_Management_System.Controllers.Admin
{
    [Admin_Authentication]
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;

        public OrdersController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _productService.GetAllOrders();
                return View(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
