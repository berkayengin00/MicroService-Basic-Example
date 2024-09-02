using Order.API.Models.Enum;

namespace Order.API.Models.Entities
{
	public class Order
	{
		public Guid Id { get; set; }
		public Guid BuyerId { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime CreatedDate { get; set; }
		public OrderStatuEnum OrderStatuEnum { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
