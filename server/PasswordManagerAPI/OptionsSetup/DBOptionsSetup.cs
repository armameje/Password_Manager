using Microsoft.Extensions.Options;
using PasswordManagerAPI.Repository.Model;

namespace PasswordManagerAPI.OptionsSetup
{
    public class DBOptionsSetup : IConfigureOptions<PasswordManagerCSOptions>
    {
        private readonly IConfiguration _configuration;

        public DBOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(PasswordManagerCSOptions options)
        {
            _configuration.GetSection(nameof(PasswordManagerCSOptions)).Bind(options);
        }
    }
}
