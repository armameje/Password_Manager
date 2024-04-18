using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Password_Manager_API.Model;
using Password_Manager_API.Repository;
using Password_Manager_API.Services;
using Serilog;

namespace Password_Manager_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.AddJsonFile("ssl.json");
            builder.Configuration.AddJsonFile("db.json");


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = 
                };
            });

            builder.Services.Configure<KeysOption>(
                builder.Configuration.GetSection(KeysOption.Key));

            builder.Services.Configure<ConnectionStringOption>(
                builder.Configuration.GetSection(ConnectionStringOption.Key));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPlatformService, PlatformService>();
            builder.Services.AddScoped<IRSAService, RSAService>();
            builder.Services.AddScoped<IHashingService, HashingService>();

            const string CorsPolicyName = "AllowAll";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Host.UseSerilog((context, config) => 
                config.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(CorsPolicyName);

            app.MapControllers();

            app.Run();
        }
    }
}
