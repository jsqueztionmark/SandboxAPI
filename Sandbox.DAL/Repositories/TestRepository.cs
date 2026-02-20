using Local.Core.Data.Repository;
using Sandbox.DAL.DB;
using Sandbox.DAL.Entities;

namespace Sandbox.DAL.Repositories;

public class TestRepository :
	IdRepositoryBase<TestEntity, int, SandboxDBContext>, ITestRepository
{
	public TestRepository(SandboxDBContext context) : base(context)
	{
	}
}