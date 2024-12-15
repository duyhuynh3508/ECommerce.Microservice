using ECommerce.Microservice.SharedLibrary.ServiceRegistration;
using ECommerce.Microservice.UserService.Api.DatabaseDbContext;
using ECommerce.Microservice.UserService.Api.JwtHelper;
using ECommerce.Microservice.UserService.Api.Mapping;
using ECommerce.Microservice.UserService.Api.Repositories;
using ECommerce.Microservice.UserService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SharedServiceRegistration.AddSharedServices<UserDbContext>(builder.Services, builder.Configuration, builder.Configuration["MySerilog:FileName"]!);

//Auth
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<JwtHandler>();

//User
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserMapping, UserMapping>();

//Role
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRoleMapping, UserRoleMapping>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
