using Microsoft.EntityFrameworkCore;
using Sandbox.DAL.Entities;

namespace Sandbox.DAL.DB;

public class SandboxDBContext : DbContext
{
	public SandboxDBContext(DbContextOptions<SandboxDBContext> options) : base(options)
	{ }
	
	public required DbSet<TestEntity> TestEntities {get; set;}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("Test_Schema");
		modelBuilder.Entity<TestEntity>(e => e.ToTable("TEST_TABLE_A"));
		base.OnModelCreating(modelBuilder);
	}
}