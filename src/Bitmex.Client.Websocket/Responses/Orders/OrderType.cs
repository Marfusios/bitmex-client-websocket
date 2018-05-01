namespace Bitmex.Client.Websocket.Responses.Orders
{
    public enum OrderType
    {
        Undefined,
        Limit,
        Market,
        Stop,
        TrailingStop,
        ExchangeMarket,
        ExchangeLimit,
        ExchangeStop,
        ExchangeTrailingStop,
        Fok,
        ExchangeFok,
        StopLimit,
        ExchangeStopLimit
    }
}
