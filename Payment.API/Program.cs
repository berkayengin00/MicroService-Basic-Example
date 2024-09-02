using MassTransit;
using Payment.API.Consumers;
using Shared;
using Stock.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(conf =>
{
	conf.AddConsumer<StockReservedEventConsumer>();
	conf.UsingRabbitMq((context, _conf) =>
	{
		_conf.Host(builder.Configuration["RabbitMQ"]);
		_conf.ReceiveEndpoint(RabbitMQSettings.Payment_StockReservedEventQueue, e => e.ConfigureConsumer<StockReservedEventConsumer>(context));

	});
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
