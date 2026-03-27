using Microsoft.Extensions.Logging;
using Sandbox.Business.Mapping;
using Sandbox.Business.Models;
using Sandbox.DAL.Entities;
using Sandbox.DAL.Repositories;

namespace Sandbox.Business.Services;

public interface ITestService
{
	IEnumerable<TestModel?> GetAllTestyThings();
}

public class TestService : ITestService
{
	private readonly ILogger<TestService> _logger;
	private readonly ITestRepository _testRepository;
	private readonly TestEntityConverter _testEntityConverter;
	
	public TestService(ILogger<TestService> logger, 
						ITestRepository testRepository,
						TestEntityConverter converter)
	{
		_logger = logger;
		_testRepository = testRepository;
		_testEntityConverter = converter;
	}
	
	public IEnumerable<TestModel?> GetAllTestyThings()
	{
		try
		{
			var entities = _testRepository.GetAll();
			var retVal = _testEntityConverter.ToModels(entities);
			return retVal;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			throw;
		}
		
	}
}