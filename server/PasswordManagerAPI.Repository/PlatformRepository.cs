using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PasswordManagerAPI.Repository.Model;
using System.Data;

namespace PasswordManagerAPI.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly PasswordManagerCSOptions _options;

        public PlatformRepository(IOptions<PasswordManagerCSOptions> options)
        {
            _options = options.Value;
        }

        public async Task AddPlatformAsync(PlatformDetails platform)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_AddPlatform",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@User",
                        Value = platform.Username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        Value = platform.PlatformName,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformUsername",
                        Value = platform.PlatformUsername,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformPasswordParam = new SqlParameter
                    {
                        ParameterName = "@PlatformPassword",
                        Value = platform.PlatformPassword,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(platformNameParam);
                    command.Parameters.Add(platformUsernameParam);
                    command.Parameters.Add(platformPasswordParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("PRIMARY KEY", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Platform already exists");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task UpdatePlatformAsync(ModifyPlatform platform)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_ModifyPlatform",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@User",
                        Value = platform.Username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        Value = platform.PlatformName,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformUsername",
                        Value = platform.PlatformUsername,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var newPlatformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@NewPlatformUsername",
                        Value = platform.NewPlatformUsername,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformPasswordParam = new SqlParameter
                    {
                        ParameterName = "@PlatformPassword",
                        Value = platform.PlatformPassword,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(platformNameParam);
                    command.Parameters.Add(platformUsernameParam);
                    command.Parameters.Add(newPlatformUsernameParam);
                    command.Parameters.Add(platformPasswordParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }

        }

        public async Task UpsertPlatformAsync(PlatformDetails platform)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_UpsertPlatform",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = platform.Username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        Value = platform.PlatformName,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformUsername",
                        Value = platform.PlatformUsername,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformPasswordParam = new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = platform.PlatformPassword,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(platformNameParam);
                    command.Parameters.Add(platformUsernameParam);
                    command.Parameters.Add(platformPasswordParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }
        }

        public async Task ChangePlatformPasswordAsync(PlatformDetails platform)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_UpsertPlatform",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = platform.Username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        Value = platform.PlatformPassword,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformUsername",
                        Value = platform.PlatformUsername,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformPasswordParam = new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = platform.PlatformPassword,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(platformNameParam);
                    command.Parameters.Add(platformUsernameParam);
                    command.Parameters.Add(platformPasswordParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }
        }

        public async Task DeletePlatformAsync(PlatformDetailsNoPassword platform)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_DeletePlatformAccount",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Direction = ParameterDirection.Input,
                        Value = platform.Username,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        Direction = ParameterDirection.Input,
                        Value = platform.PlatformName,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformUsername",
                        Direction = ParameterDirection.Input,
                        Value = platform.PlatformUsername,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(platformNameParam);
                    command.Parameters.Add(platformUsernameParam);

                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }
        }

        public async Task<PlatformDetails> GetPlatformInfoForUserAsync(PlatformDetailsNoPassword platform)
        {
            var platformDetails = new PlatformDetails
            {
                Username = platform.Username,
                PlatformName = platform.PlatformName,
                PlatformUsername = platform.PlatformUsername
            };

            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_GetPlatformAccount",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameParam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = platform.Username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformNameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformName",
                        Value = platform.PlatformName,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    var platformUsernameParam = new SqlParameter
                    {
                        ParameterName = "@PlatformUsername",
                        Value = platform.PlatformUsername,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(platformNameParam);
                    command.Parameters.Add(platformUsernameParam);

                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            platformDetails.PlatformPassword = reader["PlatformPassword"].ToString();
                        }
                    }
                }
            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }

            return platformDetails;
        }

        public async Task<List<PlatformDisplay>> GetAllPlatformsForUserAsync(string username)
        {
            var platformsForUser = new List<PlatformDisplay>();

            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_GetAllUserPlatforms",
                        CommandType = CommandType.StoredProcedure
                    };

                    var usernameparam = new SqlParameter
                    {
                        ParameterName = "@Username",
                        Value = username,
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.NVarChar
                    };

                    command.Parameters.Add(usernameparam);

                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            platformsForUser.Add(new PlatformDisplay { Username = reader["Username"].ToString(), PlatformName = reader["PlatformName"].ToString() });
                        }
                    }
                }

            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }

            return platformsForUser;
        }
    }
}
