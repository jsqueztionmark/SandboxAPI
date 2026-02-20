using AutoMapper;
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
	private readonly IMapper _mapper;
	private readonly ITestRepository _testRepository;
	
	public TestService(ILogger<TestService> logger, 
						IMapper mapper,
						ITestRepository testRepository)
	{
		_logger = logger;
		_mapper = mapper;
		_testRepository = testRepository;
	}
	
	public IEnumerable<TestModel> GetAllTestyThings()
	{
		try
		{
			var retVal = _testRepository.GetAll();
			return _mapper.Map<IEnumerable<TestModel>>(retVal);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			return null;
		}
		
	}
}