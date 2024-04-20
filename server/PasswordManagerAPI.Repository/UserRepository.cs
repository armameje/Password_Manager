using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PasswordManagerAPI.Repository.Model;
using System.Data;

namespace PasswordManagerAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PasswordManagerCS _options;

        public UserRepository(IOptions<PasswordManagerCS> options)
        {
            _options = options.Value;
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            int numberOfAlike = 0;

            try
            {
                using (var connection = new SqlConnection(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "users.usp_IsUsernameTaken",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);

                    connection.Open();

                    numberOfAlike = Convert.ToInt32(await command.ExecuteScalarAsync().ConfigureAwait(false));
                }
            }
            catch (SqlException e)
            { 
            
            }

            if (numberOfAlike == 0)
            {
                return false;
            }

            return true;
        }

        public async Task RegisterUserAsync(string username, string password, string salt, int numberOfRounds)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "users.usp_RegisterUserAccount",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = username,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    var passwordParam = new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = password,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    var saltParam = new SqlParameter
                    {
                        ParameterName = "@Salt",
                        Value = salt,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    var saltRoundsParam = new SqlParameter
                    {
                        ParameterName = "@SaltRounds",
                        Value = numberOfRounds,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(passwordParam);
                    command.Parameters.Add(saltParam);
                    command.Parameters.Add(saltRoundsParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException e)
            {
                // Add debug logging
            }
            catch (Exception e)
            {
                // Add logging
            }
        }

        public async Task RetrieveUserByUsernameAsync(string username)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "users.usp_GetUserAccountByUsername",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);

                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        { 
                            string password = reader.GetString("Password");
                            string salt = reader.GetString("Salt");
                            int numberOfRounds = reader.GetInt32("NumberOfSaltRounds");
                        }
                    }
                }
            }
            catch (Exception e)
            { 
            
            }
        }
    }
}
