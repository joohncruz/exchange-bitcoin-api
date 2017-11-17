namespace WebApi.Models
{
    public interface IExchangeFactory
    {
        IExchange CreateTrader(int id);
    }
}