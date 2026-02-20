using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Sandbox.Business.Models;
using Sandbox.Business.Services;

namespace Sandbox.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class TestController : Controller
{
	private readonly ILogger<WeatherController> _logger;
	private readonly IConfiguration _configuration;
	private readonly ITestService _testService;
	
	public TestController(IConfiguration configuration, ILogger<WeatherController> logger, ITestService testService)
	{
		_logger = logger;
		_configuration = configuration;
		_testService = testService;
	}

	[HttpGet(Name = "GetAllTestyThings")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[AllowAnonymous]
	public async Task<IEnumerable<TestModel>> GetAllTestyThings()
	{
		var response = _testService.GetAllTestyThings();
		return response;
	}
	
}
