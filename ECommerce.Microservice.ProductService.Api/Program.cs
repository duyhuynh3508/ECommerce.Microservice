using ECommerce.Microservice.ProductService.Api.DatabaseDbContext;
using ECommerce.Microservice.ProductService.Api.Mapping;
using ECommerce.Microservice.ProductService.Api.Repositories;
using ECommerce.Microservice.ProductService.Api.Services;
using ECommerce.Microservice.SharedLibrary.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SharedServiceRegistration.AddSharedServices<ProductDbContext>(builder.Services, builder.Configuration, builder.Configuration["MySerilog:FileName"]!);

//For Product
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductMapping, ProductMapping>();

//For Category
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryMapping, CategoryMapping>();

//For Currency
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICurrencyMapping, CurrencyMapping>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SharedServiceRegistration.UseSharedPolicies(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
