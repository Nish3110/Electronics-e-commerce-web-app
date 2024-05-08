using Product_Management_System.Models;
using Product_Management_System.Models.Admin;

namespace Product_Management_System.Services.Admin
{
    public interface IProductService
    {
        Task<bool> AddProductDetails(Product product);

        Task<IEnumerable<Product>> GetAllProduct();

        Task<Product> GetProductById(int Id);

        Task<bool> EditProductDetails(Product product);

        Task<bool> DeleteProductDetails(int product_Id);

        Task<bool> addToCart(int user_Id,int product_Id,int quantity);

        Task<IEnumerable<Cart>> cartDetails(int Id);

        Task<bool> removeCart(int cart_Id);

        Task<bool> changeQuantity(int cart_Id,int quantity);

        Task<bool> proceedToBuy(string cart_Ids);

        Task<IEnumerable<Order>> GetAllOrders();

        Task<IEnumerable<Cart>> GetUserOrder(int User_Id);
    }
}
