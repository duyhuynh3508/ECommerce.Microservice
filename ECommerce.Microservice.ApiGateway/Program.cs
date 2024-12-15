using ECommerce.Microservice.SharedLibrary.Logging;
using ECommerce.Microservice.SharedLibrary.ServiceRegistration;
using Microsoft.EntityFrameworkCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
builder.Logging.AddConsole();
Log.Logger = LoggingService.CreateLogger(builder.Configuration["MySerilog:FileName"]!);
var app = builder.Build();

await app.UseOcelot();

app.Run();
