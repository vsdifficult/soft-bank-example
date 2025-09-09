using SoftBank.Api.Contollers;
using SoftBank.Api.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc; 

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApiServices(builder.Configuration);  
        var app = builder.Build();
        
        app.Run();
    }
}