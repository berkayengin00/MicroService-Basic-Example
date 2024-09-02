using MassTransit;
using Order.API.Models.Context;
using Shared.Events;

namespace Order.API.Consumer
{
	public class PaymnetComplatedEventConsumer : IConsumer<PaymnetComplatedEvent>
	{
		private readonly OrderAPIDBContext orderAPIDBContext;

		public PaymnetComplatedEventConsumer(OrderAPIDBContext orderAPIDBContext)
		{
			this.orderAPIDBContext = orderAPIDBContext;
		}

		public async Task Consume(ConsumeContext<PaymnetComplatedEvent> context)
		{
			var order = orderAPIDBContext.Orders.FirstOrDefault(x=>x.Id == context.Message.OrderId);
			order.OrderStatuEnum = Models.Enum.OrderStatuEnum.Complated;
			await orderAPIDBContext.SaveChangesAsync();
		}
	}
}
