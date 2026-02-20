using Local.Core.Data;

namespace Sandbox.DAL.Entities;

[Serializable]
public partial class TestEntity : IdentityEntity<TestEntity>
{
	public string? Name { get; set; }
}