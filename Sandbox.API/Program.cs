using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sandbox.DAL.DB;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
	// .WriteTo.File("logfiles/hosts-.log",
	// 	fileSizeLimitBytes: 1_000_000,
	// 	rollOnFileSizeLimit: true,
	// 	rollingInterval: RollingInterval.Day,
	// 	shared: true,
	// 	flushToDiskInterval: TimeSpan.FromSeconds(1)
	// )
	.CreateBootstrapLogger();

try
{
	Log.Information("Starting Host...");
	var builder = WebApplication.CreateBuilder(args);

	var keyVaultUri = builder.Configuration["KeyVault:AZURE_KEYVAULT_URI"];
	var clientId = builder.Configuration["AZURE_CLIENT_ID_SANDBOX"];
	var clientSecret = builder.Configuration["AZURE_CLIENT_SECRET_SANDBOX"];
	var tenantId = builder.Configuration["AZURE_TENANT_ID"];

	if (string.IsNullOrEmpty(keyVaultUri))
		throw new ArgumentNullException("KeyVault:AZURE_KEYVAULT_URI");

	SecretClient azClient = new SecretClient(
		new Uri(keyVaultUri),
		new ClientSecretCredential(tenantId, clientId, clientSecret),
		new SecretClientOptions(SecretClientOptions.ServiceVersion.V7_1)
		{
			DisableChallengeResourceVerification = true
		});

	builder.Configuration.AddAzureKeyVault(azClient,
		new AzureKeyVaultConfigurationOptions() { ReloadInterval = new TimeSpan(0, 15, 0) }
	);
	
	builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
		.AddEnvironmentVariables();
	
	
	#region Services

	builder.Services.AddApiVersioning(opts =>
	{
		opts.DefaultApiVersion = new ApiVersion(1, 0);
		opts.AssumeDefaultVersionWhenUnspecified = true;
		opts.ReportApiVersions = true;
	});

	builder.Services.AddApiVersioning().AddMvc().AddApiExplorer(opts =>
	{
		opts.GroupNameFormat = "'v'VVV";
		opts.SubstituteApiVersionInUrl = true;
	});
	
	// Add services for OpenAPI and Swagger
	builder.Services.AddEndpointsApiExplorer(); // Required for Minimal APIs
	builder.Services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo
		{
			Title = "Your API Title",
			Version = "v1",
			Description = "A description of your API",
			// TermsOfService = new Uri("https://example.com/terms"),
			// Contact = new OpenApiContact { Name = "Your Name", Email = "you@example.com" },
			// License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://example.com/license") }
		});
	});
	
	builder.Services.AddOpenApi();
	builder.Services.AddControllers();
	
	#endregion
	
	#region DB Contexts

	builder.Services.AddDbContext<SandboxDBContext>(options =>
	{
		var connectionString = builder.Configuration.GetConnectionString("SandboxDB");
		var password = builder.Configuration["SandboxDBPW"];
		
		if (string.IsNullOrEmpty(connectionString))
			throw new ArgumentNullException("ConnectionString:SandboxDB");
		if (string.IsNullOrEmpty(password))
			throw new ArgumentNullException("Password:SandboxDB");

		var connStringWithPassword =
			(new SqlConnectionStringBuilder(connectionString) { Password = password }).ConnectionString;
		
		options.UseSqlServer(connStringWithPassword, options =>
		{
			options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
			options.CommandTimeout(300);
		});

	});
	
	#endregion
	
	#region App Config
	
	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{ 
		app.MapOpenApi();
		app.UseSwagger(); // Exposes the OpenAPI JSON document at /swagger/v1/swagger.json
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox API v1");
			// Optional: Customize the UI path (default is /swagger)
			// c.RoutePrefix = "swagger";
		});
	}

	//app.UseHttpsRedirection();
	app.UseAuthorization();
	app.MapControllers();
	
	#endregion
	//change file
	app.Run();

}
catch (Exception ex) when (ex is not HostAbortedException)
{
	Log.Fatal(ex, "Host terminated unexpectedly");	
}
finally
{
	Log.CloseAndFlush();
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}