using MassTransit;
using Order.API.Models.Context;
using Shared.Events;

namespace Order.API.Consumer
{
	public class PaymnetFailedEventConsumer : IConsumer<PaymnetFailedEvent>
	{
		private readonly OrderAPIDBContext orderAPIDBContext;

		public PaymnetFailedEventConsumer(OrderAPIDBContext orderAPIDBContext)
		{
			this.orderAPIDBContext = orderAPIDBContext;
		}

		public async Task Consume(ConsumeContext<PaymnetFailedEvent> context)
		{
			var order = orderAPIDBContext.Orders.FirstOrDefault(x => x.Id == context.Message.OrderId);
			order.OrderStatuEnum = Models.Enum.OrderStatuEnum.Fail;
			await orderAPIDBContext.SaveChangesAsync();
		}
	}
}
