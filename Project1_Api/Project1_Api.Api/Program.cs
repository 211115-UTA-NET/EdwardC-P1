using Project1_Api.DataStorage;

string connectionString = await File.ReadAllTextAsync("C:/Users/rootb/Revature/Database_File/ConnectBikeShop.txt");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoginRepo>(sp => new LoginRepo(connectionString));
builder.Services.AddSingleton<IStoreLocationRepo>(sp => new StoreLocationRepo(connectionString));
builder.Services.AddSingleton<IStoreInventoryRepo>(sp => new StoreInventoryRepo(connectionString));
builder.Services.AddSingleton<IInvoiceRepo>(sp => new InvoiceRepo(connectionString));
builder.Services.AddSingleton<ICustomerRepo>(sp => new CustomerRepo(connectionString));
builder.Services.AddSingleton<IItemDetailsRepo>(sp => new ItemDetailsRepo(connectionString));

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
