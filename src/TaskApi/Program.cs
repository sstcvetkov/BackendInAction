using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

builder.Host.ConfigureLogging(logging =>
	{
		logging.AddSerilog();
		logging.SetMinimumLevel(LogLevel.Information);
	})
	.UseSerilog();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();