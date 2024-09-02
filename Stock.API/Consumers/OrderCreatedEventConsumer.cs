using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Events;
using Stock.API.Models.Context;

namespace Stock.API.Consumers
{
	public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
	{
		private readonly StockAPIDBContext _context;
		private readonly ISendEndpointProvider _sendEndpointProvider;
		private readonly IPublishEndpoint publishEndpoint;
		public OrderCreatedEventConsumer(StockAPIDBContext context, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
		{
			_context = context;
			_sendEndpointProvider = sendEndpointProvider;
			this.publishEndpoint = publishEndpoint;
		}

		public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
		{
			try
			{
				Console.WriteLine($"OrderId : {context.Message.OrderId} -BuyerId : {context.Message.BuyerId}");

				List<bool> stockResult = new List<bool>();

				//var existProductStocks = await _context.Stocks.Where(x => context.Message.OrderItems.Any(y => y.ProductId == x.ProductId)).ToListAsync();

				var dbStocks = await _context.Stocks.ToListAsync();
				var existProductStocks = dbStocks.Where(x => context.Message.OrderItems.Any(y => y.ProductId == x.ProductId)).ToList();

				var notHaveStockProductList = existProductStocks
					.Where(x => context.Message.OrderItems.Any(y => y.ProductId == x.ProductId && y.Count > x.Count))
					.Select(x => x.ProductId)
					.ToList();

				var haveStockProductList = existProductStocks
					.Where(x => context.Message.OrderItems.Any(y => y.ProductId == x.ProductId && y.Count <= x.Count))
					.Select(x => x.ProductId)
					.ToList();


				if (notHaveStockProductList?.Count > 0)
				{
					var stockNotReservedEvent = new StockNotReservedEvent
					{
						BuyerId = context.Message.BuyerId,
						Message = "Ürünlerin Stokları yetersiz",
						OrderId = context.Message.OrderId
					};
					await publishEndpoint.Publish(stockNotReservedEvent);
				}
				else
				{
					existProductStocks.ForEach(x =>
					{
						x.Count -= context.Message.OrderItems.FirstOrDefault(y => y.ProductId == x.ProductId).Count;
					});
					StockReservedEvent stockReservedEvent = new StockReservedEvent()
					{
						BuyerId = context.Message.BuyerId,
						OrderId = context.Message.OrderId,
						TotalPrice = context.Message.TotalPrice
					};

					ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{ RabbitMQSettings.Payment_StockReservedEventQueue }"));
					await sendEndpoint.Send(stockReservedEvent);
					//Payment
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{

				throw;
			}

		}
	}
}
