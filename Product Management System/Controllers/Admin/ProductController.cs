using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_Management_System.Models;
using Product_Management_System.Models.Admin;
using Product_Management_System.Services;
using Product_Management_System.Services.Admin;

namespace Product_Management_System.Controllers.Admin
{
    [Admin_Authentication]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
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

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _productService.AddProductDetails(product);
                if (result == false)
                {
                    ViewBag.error_message = "Something went wrong!";
                }
                else
                {
                    ViewBag.success_message = "Product add successfully!";
                }
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int Id)
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
        public async Task<IActionResult> EditProduct(int Id, Product product)
        {
            try
            {
                bool result = false;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (Id > 0 && Id != null)
                {
                    product.Product_Id = Id;
                    result = await _productService.EditProductDetails(product);
                }
                if (!result)
                {
                    ViewBag.error_message = "Something went wrong!";
                }
                else
                {
                    ViewBag.success_message = "Product update successfully!";
                }
                return View();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int product_Id)
        {
            try
            {
                if (product_Id > 0 && product_Id != null)
                {
                    var result = await _productService.DeleteProductDetails(product_Id);
                    if (result)
                    {
                        return Json(true); // Return true if deletion is successful
                    }
                    else
                    {
                        return Json(false); // Return false if deletion fails
                    }
                }
                return Json(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailProduct(int Id)
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
    }
}
