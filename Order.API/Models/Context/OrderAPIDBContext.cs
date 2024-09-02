using Microsoft.EntityFrameworkCore;

namespace Order.API.Models.Context
{
	public class OrderAPIDBContext : DbContext
	{
		public OrderAPIDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Entities.Order> Orders { get; set; }
		public DbSet<Entities.OrderItem> OrderItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
