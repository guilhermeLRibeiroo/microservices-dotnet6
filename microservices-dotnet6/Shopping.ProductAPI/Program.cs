using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.ProductAPI.Config;
using Shopping.ProductAPI.Models.Context;
using Shopping.ProductAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31))));

IMapper mapper = MappingConfig
    .RegisterMaps()
    .CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
