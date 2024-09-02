using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Stock.API.Consumers;
using Stock.API.Models.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(conf =>
{
	conf.AddConsumer<OrderCreatedEventConsumer>();
	conf.UsingRabbitMq((context, _conf) =>
	{
		_conf.Host(builder.Configuration["RabbitMQ"]);
		_conf.ReceiveEndpoint(RabbitMQSettings.Stock_OrderCreatedEventQueue , e=>e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
	
	});
});

builder.Services.AddDbContext<StockAPIDBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
