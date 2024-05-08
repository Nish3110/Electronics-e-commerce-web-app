using Product_Management_System.Models;
using System.Data;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Collections.Generic;
using Product_Management_System.Models.Admin;

namespace Product_Management_System.Repository
{
    public class UserRepository: IUserRepository
    {
        private IConfiguration configuration;

        public UserRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
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


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@sp_operation", "SELECT", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    var result = await connection.QueryAsync<User>("sp_UserDetails", parameter, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> SaveUserDetail(User user)
        {
            try
            {
                var parameter = new DynamicParameters();

                byte[] encode = new byte[user.Password.Length];
                encode = Encoding.UTF8.GetBytes(user.Password);
                string Password = Convert.ToBase64String(encode);

                parameter.Add("@Firstname", user.Firstname, DbType.String, ParameterDirection.Input);
                parameter.Add("@Lastname", user.Lastname, DbType.String, ParameterDirection.Input);
                parameter.Add("@Birthdate", Convert.ToDateTime(user.Birthdate).ToString("MM/dd/yyyy"), DbType.String, ParameterDirection.Input);
                parameter.Add("@Gender", user.Gender, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Email", user.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@Phonenumber", user.Phonenumber, DbType.String, ParameterDirection.Input);
                parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
                parameter.Add("@sp_operation", "INSERT", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_UserDetails", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserDetail(string email, string password)
        {
            var parameter = new DynamicParameters();

            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            string Password = Convert.ToBase64String(encode);

            parameter.Add("@Email", email, DbType.String, ParameterDirection.Input);
            parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                await ((DbConnection)connection).OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<User>("sp_UserLogin", parameter, commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async Task<User> GetUserDetailById(int user_Id)
        {
            var parameter = new DynamicParameters();

            parameter.Add("@User_Id", user_Id, DbType.String, ParameterDirection.Input);
            parameter.Add("@sp_operation", "GET", DbType.String, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                await ((DbConnection)connection).OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<User>("sp_UserDetails", parameter, commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async Task<bool> EditUserDetail(User user)
        {
            try
            {
                var parameter = new DynamicParameters();

                byte[] encode = new byte[user.Password.Length];
                encode = Encoding.UTF8.GetBytes(user.Password);
                string Password = Convert.ToBase64String(encode);

                parameter.Add("@Firstname", user.User_Id, DbType.String, ParameterDirection.Input);
                parameter.Add("@Firstname", user.Firstname, DbType.String, ParameterDirection.Input);
                parameter.Add("@Lastname", user.Lastname, DbType.String, ParameterDirection.Input);
                parameter.Add("@Birthdate", Convert.ToDateTime(user.Birthdate).ToString("MM/dd/yyyy"), DbType.String, ParameterDirection.Input);
                parameter.Add("@Gender", user.Gender, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Email", user.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@Phonenumber", user.Phonenumber, DbType.String, ParameterDirection.Input);
                parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
                parameter.Add("@sp_operation", "UPDATE", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_UserDetails", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserDetail(int user_Id)
        {
            try
            {
                var parameter = new DynamicParameters();

                parameter.Add("@User_Id", user_Id, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@sp_operation", "DELETE", DbType.String, ParameterDirection.Input);
                using (IDbConnection connection = GetDbConnection())
                {
                    await ((DbConnection)connection).OpenAsync();
                    await connection.ExecuteAsync("sp_UserDetails", parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
