using TaskApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console(outputTemplate:
		"[{Timestamp:HH:mm:ss} {Level:u3} {CorrelationId}] {Message}{NewLine}{Exception}")
	.Enrich.FromLogContext()
	.CreateLogger();

builder.Host.ConfigureLogging(logging =>
	{
		logging.AddSerilog();
		logging.SetMinimumLevel(LogLevel.Information);
	})
	.UseSerilog();

builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
app.MapControllers();
app.Run();