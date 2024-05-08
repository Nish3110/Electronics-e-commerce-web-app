using Dapper;
using Product_Management_System.Models.Admin;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.Reflection.Metadata;
using Product_Management_System.Models;

namespace Product_Management_System.Repository.Admin
{
    public class ProductRepository : IProductRepository
    {
        private IConfiguration configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public ProductRepository(IConfiguration _configuration, IWebHostEnvironment hostingEnvironment)
        {
            configuration = _configuration;
            _hostingEnvironment = hostingEnvironment;

        }


        public DbConnection GetDbConnection()
        {
            string connectionstringAppSetting = configuration.GetConnectionString("connectionstring");
            if (string.IsNullOrEmpty(connectionstringAppSetting))
            {
                return new SqlConnection(connectionstringAppSetting);
            }
            return new SqlConnection(connectionstringAppSetting);
        }


        public async Task<bool> AddProductDetails(Product product)
        {
            try
            {
                var parameter = new DynamicParameters();
                List<string> imagePathList = new List<string>();
                if (product.Images != null)
                {
                    foreach (var image in product.Images)
                    {
                        string uniqueFilename = Guid.NewGuid().ToString().Replace("-", "");
                        var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                        string[] extension = image.ContentType.Split('/');
                        string filePath = Path.Combine(directoryPath, uniqueFilename + '.' + extension[1]);
                        Directory.CreateDirectory(directoryPath);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        imagePathList.Add("Images/" + uniqueFilename + '.' + extension[1]);
                    }
                }

                if (product.Cover_Image != null)
                {
                    string uniqueFilename = Guid.NewGuid().ToString().Replace("-", "");
                    var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    string[] extension = product.Cover_Image.ContentType.Split('/');
                    string filePath = Path.Combine(directoryPath, uniqueFilename + '.' + extension[1]);
                    Directory.CreateDirectory(directoryPath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Cover_Image.CopyToAsync(fileStream);
                    }
                    product.Product_Cover_Image = "Images/" + uniqueFilename + '.' + extension[1];
                }
                product.Product_Images = string.Join(',', imagePathList);
                parameter.Add("@Product_Name", product.Product_Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Description", product.Product_Description, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Price", product.Product_Price, DbType.Decimal, ParameterDirection.Input);
                parameter.Add("@Product_Cover_Image", product.Product_Cover_Image, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Images", product.Product_Images, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Quantity", product.Product_Quantity, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@Product_Status", product.Product_Status, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@sp_operation", "INSERT", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Product_Details", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@sp_operation", "SELECT", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    var result = await connection.QueryAsync<Product>("sp_Product_Details", parameter, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product> GetProductById(int Id)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Product_Id", Id, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@sp_operation", "GET", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    var result = await connection.QueryFirstOrDefaultAsync<Product>("sp_Product_Details", parameter, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> EditProductDetails(Product product)
        {
            try
            {
                var parameter = new DynamicParameters();
                List<string> imagePathList = new List<string>();
                if (product.Images != null)
                {
                    foreach (var image in product.Images)
                    {
                        string uniqueFilename = Guid.NewGuid().ToString().Replace("-", "");
                        var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                        string[] extension = image.ContentType.Split('/');
                        string filePath = Path.Combine(directoryPath, uniqueFilename + '.' + extension[1]);
                        Directory.CreateDirectory(directoryPath);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        imagePathList.Add("Images/" + uniqueFilename + '.' + extension[1]);
                    }
                }
                string[] productImagesArray = product.Product_Images.Split(',');
                string[] combinedArray = productImagesArray.Concat(imagePathList).ToArray();
                product.Product_Images = string.Join(',', combinedArray);

                if (product.Cover_Image != null)
                {
                    string uniqueFilename = Guid.NewGuid().ToString().Replace("-", "");
                    var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    string[] extension = product.Cover_Image.ContentType.Split('/');
                    string filePath = Path.Combine(directoryPath, uniqueFilename + '.' + extension[1]);
                    Directory.CreateDirectory(directoryPath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Cover_Image.CopyToAsync(fileStream);
                    }
                    product.Product_Cover_Image = "Images/" + uniqueFilename + '.' + extension[1];
                }

                parameter.Add("@Product_Id", product.Product_Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Product_Name", product.Product_Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Description", product.Product_Description, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Price", product.Product_Price, DbType.Decimal, ParameterDirection.Input);
                parameter.Add("@Product_Cover_Image", product.Product_Cover_Image, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Images", product.Product_Images, DbType.String, ParameterDirection.Input);
                parameter.Add("@Product_Quantity", product.Product_Quantity, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@Product_Status", product.Product_Status, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@sp_operation", "UPDATE", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Product_Details", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteProductDetails(int product_Id)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Product_Id", product_Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@sp_operation", "DELETE", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Product_Details", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> addToCart(int user_Id, int product_Id, int quantity)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@User_Id", user_Id, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@Product_Id", product_Id, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@Quantity", quantity, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@sp_operation", "INSERT", DbType.String, ParameterDirection.Input);

                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Cart_Details", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<IEnumerable<Cart>> cartDetails(int Id)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@User_Id", Id, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@sp_operation", "SELECT", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    var result = await connection.QueryAsync<Cart>("sp_Cart_Details", parameter, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> removeCart(int cart_Id)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Cart_Id", cart_Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@sp_operation", "DELETE", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Cart_Details", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> changeQuantity(int cart_Id,int quantity)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@Cart_Id", cart_Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Quantity", quantity, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@sp_operation", "UPDATE", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Cart_Details", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> proceedToBuy(string cart_Ids)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Cart_Ids", cart_Ids, DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_Order_Completed", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    var result = await connection.QueryAsync<Order>("sp_Get_Order_Details", null, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<IEnumerable<Cart>> GetUserOrder(int User_Id)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@User_Id", User_Id, DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    var result = await connection.QueryAsync<Cart>("sp_Get_User_Order_Details", parameter, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
