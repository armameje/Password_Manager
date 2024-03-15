using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using Password_Manager_API.Model;

namespace Password_Manager_API.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task RegisterUserAsync(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "usp_RegisterUser",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    SqlParameter passwordParam = new SqlParameter
                    {
                        ParameterName = "@Password",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = password
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(passwordParam);

                    connection.Open();
                    var x = await command.ExecuteNonQueryAsync();

                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<UserInfo> RetrieveUserAsync(string username)
        {
            UserInfo user = new UserInfo();

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "usp_GetUserByUsername",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    command.Parameters.Add(usernameParam);

                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            user.UserID = (int)reader["iUserID"];
                            user.Username = reader["vcUserName"].ToString();
                            user.Password = reader["nvcPassword"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return user;
        }
    }
}
