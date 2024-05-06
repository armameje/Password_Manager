using Microsoft.Extensions.Options;
using PasswordManagerAPI.Services.Models;

namespace PasswordManagerAPI.OptionsSetup
{
    public class PEMOptionsSetup : IConfigureOptions<PEMOptions>
    {
        private readonly IConfiguration _configuration;

        public PEMOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(PEMOptions options)
        {
            _configuration.GetSection(nameof(PEMOptions)).Bind(options);
        }
    }
}
