namespace Order.API.Models.VMs
{
	public class OrderAddVM
	{
		public Guid BuyerId { get; set; }
		public List<OrderItemAddVM> OrderItems { get; set; }
	}

	public class OrderItemAddVM
	{
		public Guid BuyerId { get; set; }
		public Guid ProductId { get; set; }
		public int Count { get; set; }
		public decimal Price { get; set; }

	}
}
