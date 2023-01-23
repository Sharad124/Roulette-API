using Microsoft.EntityFrameworkCore;
using Roulette_API.Models;
using Roulette_API.Context;
using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
    var builder = WebApplication.CreateBuilder(args);
    

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    var connectionString = builder.Configuration.GetConnectionString("RouletteDB");
    builder.Services.AddDbContext<RouletteContext>(i => i.UseSqlServer(connectionString));

    //builder.Services.AddDbContext<RouletteContext>(i => i.UseInMemoryDatabase("RouletteDB"));
    builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}