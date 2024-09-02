namespace Shared
{
	public static class RabbitMQSettings
	{
		public const string Stock_OrderCreatedEventQueue = "stock-order-created-event-queue";
		public const string Payment_StockReservedEventQueue = "payment-stock-reserved-event-queue";
		public const string Order_PaymnetComplatedEventQueue = "order-payment-complated-event-queue";
		public const string Order_StockNotReservedEventQueue = "order-stock-not-reserved-event-queue";
		public const string Order_PaymnetFailedEventQueue = "order-payment-failed-event-queue";
	}
}
