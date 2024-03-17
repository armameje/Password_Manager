using System.Data.SqlClient;
using System.Data;
using Password_Manager_API.Model;
using System.Security.Principal;
using Microsoft.Extensions.Options;

namespace Password_Manager_API.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ConnectionStringOption _options;

        public PlatformRepository(IOptions<ConnectionStringOption> options)
        {
            _options = options.Value;
        }

        public async Task<Dictionary<string, int>> RetrieveAllPlatformAsync()
        {
            var platforms = new Dictionary<string, int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_options.PasswordManagerDB))
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "usp_GetAllPlatforms"
                    };

                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int platformID = (int)reader["iPlatformID"];
                            string platformName = reader["vcPlatformName"].ToString();
                            platforms.Add(platformName, platformID);
                        };
                    }
                };
            }
            catch (Exception e)
            { 
            
            }

            return platforms;
        }

        public async Task AddPlatformAsync(string platformName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_options.PasswordManagerDB))
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "usp_InsertPlatform"
                    };

                    SqlParameter platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = platformName
                    };

                    command.Parameters.Add(platformNameParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }

            }
            catch (Exception e)
            { 
            
            }
        }

        public async Task AddAccountToPlatformAsync(UserPlatformAccount userAccount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_options.PasswordManagerDB))
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "usp_RegisterAccount"
                    };

                    SqlParameter usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = userAccount.Account.AccountUsername
                    };

                    SqlParameter passwordParam = new SqlParameter
                    {
                        ParameterName = "@Password",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = userAccount.Account.Password
                    };

                    var platforms = await RetrieveAllPlatformAsync();
                    var platformID = platforms.Where(x => x.Key.Equals(userAccount.Account.PlatformName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().Value;

                    SqlParameter platformIDParam = new SqlParameter
                    {
                        ParameterName = "@PlatformID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = platformID
                    };

                    var userRepo = new UserRepository();
                    var user = await userRepo.RetrieveUserAsync(userAccount.Username);

                    SqlParameter userIDParam = new SqlParameter
                    {
                        ParameterName = "@UserID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = user.UserID
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(passwordParam);
                    command.Parameters.Add(platformIDParam);
                    command.Parameters.Add(userIDParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            { 
            
            }
        }

        public async Task<List<PlatformAccount>> GetAllAccountsOfUserAsync(string username)
        {
            var platformAccounts = new List<PlatformAccount>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_options.PasswordManagerDB))
                {
                    SqlCommand command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "usp_GetAllAccountsOfUser"
                    };

                    SqlParameter usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = username,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };


                    command.Parameters.Add(usernameParam);

                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            string platformUsername = reader["PlatformUsername"].ToString();
                            string platformName = reader["PlatformName"].ToString();
                            string platformPassword = reader["PlatformPassword"].ToString();

                            platformAccounts.Add(new PlatformAccount
                            {
                                AccountUsername = platformUsername,
                                PlatformName = platformName,
                                Password = platformPassword
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return platformAccounts;
        }
    }
}
