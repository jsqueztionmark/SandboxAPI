using Microsoft.Extensions.Logging;
using Sandbox.Business.Models;
using Sandbox.DAL.Repositories;

namespace Sandbox.Business.Services;

public interface ITestService
{
	IEnumerable<TestModel> GetAllTestyThings();
}

public class TestService : ITestService
{
	private readonly ILogger<TestService> _logger;
	private readonly ITestRepository _testRepository;
	
	public TestService(ILogger<TestService> logger, 
						ITestRepository testRepository)
	{
		_logger = logger;
		_testRepository = testRepository;
	}
	
	public IEnumerable<TestModel> GetAllTestyThings()
	{
		try
		{
			var retVal = _testRepository.GetAll();
			return null;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			return null;
		}
		
	}
}