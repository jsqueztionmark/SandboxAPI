using Microsoft.EntityFrameworkCore;
using Sandbox.DAL.Entities;

namespace Sandbox.DAL.DB;

public class SandboxDBContext : DbContext
{
	public SandboxDBContext(DbContextOptions<SandboxDBContext> options) : base(options)
	{ }
	
	public required DbSet<TestEntity> TEST_TABLE_A {get; set;}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TestEntity>();
		base.OnModelCreating(modelBuilder);
	}
}