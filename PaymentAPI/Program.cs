using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Configure Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure DbContext
        builder.Services.AddDbContext<PaymentDetailContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

        // Build the application
        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // CORS configuration
        app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader());

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
