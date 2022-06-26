using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.IO;
using System.Text;

namespace FlightManegment
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            });
            services.AddOcelot(_configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value))
            });

            //services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            //var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json")
            //     .Build(); var connectionString = configuration.GetConnectionString("AppDb");
            //optionsBuilder.UseSqlServer(connectionString);
            // Action<ConfigurationBuilderCachePart> settings = (x) =>
            //{
            //    x.WithMicrosoftLogging(log =>
            //    {
            //        log.AddConsole(LogLevel.Debug);
            //    }).WithDictionaryHandle();
            //};
            //services.AddOcelot(Configuration, settings);



        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseAuthentication();
            await app.UseOcelot();
        }
    }
}
