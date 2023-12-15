using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using freshiehubAPI.Models;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<FreshieDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "freshie API"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
