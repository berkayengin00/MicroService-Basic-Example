using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
	public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
	{
		private readonly IPublishEndpoint publishEndpoint;

		public StockReservedEventConsumer(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		public Task Consume(ConsumeContext<StockReservedEvent> context)
		{
			if (true) //Ödeme Tamam
			{
				PaymnetComplatedEvent paymnetComplatedEvent = new PaymnetComplatedEvent()
				{
					OrderId = context.Message.OrderId,
				};
				publishEndpoint.Publish(paymnetComplatedEvent);
			}
			else // Hata
			{
				PaymnetFailedEvent paymnetFailedEvent = new()
				{
					OrderId = context.Message.OrderId,
					Message = "Para Yok"
				};
				publishEndpoint.Publish(paymnetFailedEvent);

			}
			return Task.CompletedTask;
		}
	}
}
