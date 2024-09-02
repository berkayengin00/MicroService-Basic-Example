using Microsoft.EntityFrameworkCore;

namespace Stock.API.Models.Context
{
	public class StockAPIDBContext : DbContext
	{
		public StockAPIDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Entities.Stock> Stocks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region Seed Data
			//var stockList = new List<Entities.Stock>()
			//{
			//	new()
			//	{
			//		Id=Guid.NewGuid(),
			//		ProductId = Guid.NewGuid(),
			//		Count = 10
			//	},
			//	new()
			//	{
			//		Id=Guid.NewGuid(),
			//		ProductId = Guid.NewGuid(),
			//		Count = 5
			//	},
			//	new()
			//	{
			//		Id=Guid.NewGuid(),
			//		ProductId = Guid.NewGuid(),
			//		Count = 2
			//	},
			//	new()
			//	{
			//		Id=Guid.NewGuid(),
			//		ProductId = Guid.NewGuid(),
			//		Count = 4
			//	},

			//};
			//modelBuilder.Entity<Entities.Stock>().HasData(stockList); 
			#endregion
			base.OnModelCreating(modelBuilder);
		}
	}
}
