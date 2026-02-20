using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Sandbox.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class WeatherController : Controller
{
	private readonly ILogger<WeatherController> _logger;
	private readonly IConfiguration _configuration;
	
	public WeatherController(IConfiguration configuration, ILogger<WeatherController> logger)
	{
		_logger = logger;
		_configuration = configuration;
	}

	[HttpGet(Name = "GetWeatherForecast")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[AllowAnonymous]
	public async Task<WeatherForecast[]> GetWeatherForecastAsync([FromQuery] DateTime? date)
	{
		var summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		
		var forecast = Enumerable.Range(1, 5).Select(index =>
				new WeatherForecast
				(
					DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					Random.Shared.Next(-20, 55),
					summaries[Random.Shared.Next(summaries.Length)]
				))
			.ToArray();
		
		return forecast;
	}
	
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}