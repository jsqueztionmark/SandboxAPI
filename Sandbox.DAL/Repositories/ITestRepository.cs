using Local.Core.Data.Repository;
using Sandbox.DAL.Entities;

namespace Sandbox.DAL.Repositories;

public interface ITestRepository : IGetAll<TestEntity>
{
	
}