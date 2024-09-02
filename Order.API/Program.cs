using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Consumer;
using Order.API.Models.Context;
using Shared;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<OrderAPIDBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(conf =>
{
	conf.AddConsumer<PaymnetComplatedEventConsumer>();
	conf.AddConsumer<StockNotReservedEventConsumer>();
	conf.AddConsumer<PaymnetFailedEventConsumer>();
	conf.UsingRabbitMq((context, _conf) =>
	{
		_conf.Host(builder.Configuration["RabbitMQ"]);
		_conf.ReceiveEndpoint(RabbitMQSettings.Order_PaymnetComplatedEventQueue,x=>x.ConfigureConsumer<PaymnetComplatedEventConsumer>(context));
		_conf.ReceiveEndpoint(RabbitMQSettings.Order_StockNotReservedEventQueue,x=>x.ConfigureConsumer<StockNotReservedEventConsumer>(context));
		_conf.ReceiveEndpoint(RabbitMQSettings.Order_PaymnetFailedEventQueue,x=>x.ConfigureConsumer<PaymnetFailedEventConsumer>(context));
	});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
