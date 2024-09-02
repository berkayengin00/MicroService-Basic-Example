using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models.Context;
using Order.API.Models.VMs;
using Shared.Events;

namespace Order.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly OrderAPIDBContext _context;
		private readonly IPublishEndpoint _publishEndpoint;

		public OrdersController(OrderAPIDBContext context, IPublishEndpoint publishEndpoint)
		{
			_context = context;
			_publishEndpoint = publishEndpoint;
		}

		[HttpPost]
		public async Task<IActionResult> AddOrder(OrderAddVM model)
		{
			#region Add Order
			var order = new Models.Entities.Order()
			{
				BuyerId = model.BuyerId,
				CreatedDate = DateTime.Now,
				OrderStatuEnum = Models.Enum.OrderStatuEnum.Suspend
			};

			order.OrderItems = model.OrderItems.Select(x => new Models.Entities.OrderItem()
			{
				ProductId = x.ProductId,
				Count = x.Count,
				Price = x.Price
			}).ToList();

			order.TotalPrice = order.OrderItems.Sum(x => x.Price * x.Count);

			await _context.AddAsync(order);
			var result = await _context.SaveChangesAsync();
			#endregion

			OrderCreatedEvent orderCreatedEvent = new OrderCreatedEvent()
			{
				BuyerId = order.BuyerId,
				OrderId = order.Id,
				OrderItems = order.OrderItems.Select(x=> new Shared.Messages.OrderItemMessage
				{
					Count = x.Count,
					ProductId = x.ProductId
				}).ToList(),
				TotalPrice = order.TotalPrice,				
			};

			await _publishEndpoint.Publish(orderCreatedEvent);

			return Ok(result>0);
		}
	}
}
