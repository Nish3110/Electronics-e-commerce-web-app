using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Product_Management_System.Models;

namespace Product_Management_System.Services
{
    public interface IUserService
    {
        Task<bool> SaveUserDetail(User user);

        Task<User> GetUserDetail(string email, string password);

        Task<User> GetUserDetailById(int user_Id);

        Task<bool> EditUserDetail(User user);

        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> DeleteUserDetail(int user_Id);
    }
}
