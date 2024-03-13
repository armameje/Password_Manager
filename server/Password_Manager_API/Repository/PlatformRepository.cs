using System.Data.SqlClient;
using System.Data;
using Password_Manager_API.Model;

namespace Password_Manager_API.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        public async Task<Dictionary<string, int>> RetrieveAllPlatformAsync()
        {
            var platforms = new Dictionary<string, int>();

            try
            {
                using (SqlConnection connection = new SqlConnection())
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
                using (SqlConnection connection = new SqlConnection())
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

            }
            catch (Exception e)
            { 
            
            }
        }
    }
}
