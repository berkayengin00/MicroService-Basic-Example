using MassTransit;
using Order.API.Models.Context;
using Shared.Events;

namespace Order.API.Consumer
{
	public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
	{
		private readonly OrderAPIDBContext orderAPIDBContext;

		public StockNotReservedEventConsumer(OrderAPIDBContext orderAPIDBContext)
		{
			this.orderAPIDBContext = orderAPIDBContext;
		}

		public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
		{
			var order = orderAPIDBContext.Orders.FirstOrDefault(x => x.Id == context.Message.OrderId);
			order.OrderStatuEnum = Models.Enum.OrderStatuEnum.Fail;
			await orderAPIDBContext.SaveChangesAsync();
		}
	}
}
