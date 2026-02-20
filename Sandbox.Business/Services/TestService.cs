using Sandbox.Business.Models;

namespace Sandbox.Business.Services;

public interface ITestService
{
	IEnumerable<TestModel> GetAllTestyThings();
}

public class TestService : ITestService
{
	public IEnumerable<TestModel> GetAllTestyThings()
	{
		throw new NotImplementedException();
	}
}