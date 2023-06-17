#region Namespaces
using Microsoft.EntityFrameworkCore;
using Npgsql;
using imaginnovatetest.Models;
using imaginnovatetest.Interfaces;
using imaginnovatetest.Repository;
using Microsoft.AspNetCore.Hosting;
#endregion
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ImaginnovateDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<Iemployeedetails, emprepository>();
builder.Services.AddCors();

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

