using System.Reflection;
using System.Text;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Infrastructure.Auth; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using SoftBank.Core.Services.BFF;

namespace SoftBank.Api.Infra;

public static class ApiExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddControllers(); 
        // ConfigureSwagger(services); 
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAccountBFFService, AccountBFFService>();

        // services.AddScoped<IDataService, DataService>(); 
        services.AddScoped<ICardBFFService, CardBFFService>();
        services.AddScoped<IClientBFFService, ClientBFFService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
        services.AddAuthentication();
        services.Configure<SoftBank.Core.Email.SmtpOptions>(configuration.GetSection("Smtp"));
        services.AddScoped<SoftBank.Core.Email.EmailSenderService>();
        return services;
    }
    public static void UseApiMiddlewares(this WebApplication app, IWebHostEnvironment env)
    {
        app.UseStaticFiles();
        app.UseSwagger(c => { c.RouteTemplate = "api-docs/{documentName}/swagger.json"; });
        app.UseSwaggerUI(
            c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Test bank system");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                c.DefaultModelExpandDepth(0);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.EnableValidator();
                c.DocumentTitle = "Test Soft Bank System";
            }
        );
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGet("/", () => Results.Redirect("/api-docs/v1"))
            .ExcludeFromDescription();
    } 
    
}