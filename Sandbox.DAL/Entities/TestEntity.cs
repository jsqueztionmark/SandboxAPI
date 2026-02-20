using Local.Core.Data.Entity;

namespace Sandbox.DAL.Entities;

[Serializable]
public partial class TestEntity : IdentityEntity<int>
{
	public string? Name { get; set; }
}