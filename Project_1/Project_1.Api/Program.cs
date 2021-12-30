using Project_1.Api.Models;
using Project_1.Api.Repositories;
//using Project_1.Api.DataStorage;
//using Project_1Api.DataStorage;
using Microsoft.EntityFrameworkCore;


//string connectionString = await File.ReadAllTextAsync("C:/User/rootb/Revature/Database_File/ConnectBikeShop.txt");
//IRepository repository = new SqlRepository(connectionString);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddDbContext<CustomerContext>(opt =>
    opt.UseInMemoryDatabase("Data source=Customers.db"));
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
