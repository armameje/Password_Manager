﻿using Microsoft.Data.SqlClient;
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
                        Direction= ParameterDirection.Input,
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

        public async Task DeletePlatformAsync(PlatformDetails platform)
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
                        Value = platform.Username,
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

        public async Task GetPlatformInfoForUserAsync(PlatformDetails platform)
        {
            try
            {
                using (SqlConnection connection = new(_options.ConnectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "platforms.usp_ChangePlatformPassword",
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

                    var platformUsername = new SqlParameter
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
                    command.Parameters.Add(platformNameParam);
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
    }
}
