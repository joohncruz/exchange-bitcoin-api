using System;

namespace WebApi.Models
{
    public class ExchangeFactory : IExchangeFactory
    {
        public IExchange CreateTrader(int id)
        {
            switch (id)
            {
                case 1: return new FoxBit();
                case 2: return new MecBit();
                case 3: return new B2U();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}