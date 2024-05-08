using Product_Management_System.Models;
using Product_Management_System.Models.Admin;
using Product_Management_System.Repository.Admin;

namespace Product_Management_System.Services.Admin
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> AddProductDetails(Product product)
        {
            return await _productRepository.AddProductDetails(product);
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _productRepository.GetAllProduct();
        }

        public async Task<Product> GetProductById(int Id)
        {
            return await _productRepository.GetProductById(Id);
        }

        public async Task<bool> EditProductDetails(Product product)
        {
            return await _productRepository.EditProductDetails(product);
        }

        public async Task<bool> DeleteProductDetails(int product_Id)
        {
            return await _productRepository.DeleteProductDetails(product_Id);
        }

        public async Task<bool> addToCart(int user_Id, int product_Id, int quantity)
        {
            return await _productRepository.addToCart(user_Id,product_Id, quantity);
        }

        public async Task<IEnumerable<Cart>> cartDetails(int Id)
        {
            return await _productRepository.cartDetails(Id);
        }

        public async Task<bool> removeCart(int cart_Id)
        {
            return await _productRepository.removeCart(cart_Id);
        }

        public async Task<bool> changeQuantity(int cart_Id, int quantity)
        {
            return await _productRepository.changeQuantity(cart_Id,quantity);
        }

        public async Task<bool> proceedToBuy(string cart_Ids)
        {
            return await _productRepository.proceedToBuy(cart_Ids);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _productRepository.GetAllOrders();
        }

        public async Task<IEnumerable<Cart>> GetUserOrder(int User_Id)
        {
            return await _productRepository.GetUserOrder(User_Id);
        }
    }
}
