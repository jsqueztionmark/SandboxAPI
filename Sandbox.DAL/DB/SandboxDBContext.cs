using Microsoft.EntityFrameworkCore;

namespace Sandbox.DAL.DB;

public class SandboxDBContext : DbContext
{
	public SandboxDBContext(DbContextOptions<SandboxDBContext> options) : base(options)
	{
	}
}