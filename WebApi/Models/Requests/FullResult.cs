namespace WebApi.Models.Requests
{
    public class FullResult
    {
        public CalcularValoresRequest calcularValoresRequest { get; set; }
        public CalcularValoresResult calcularValoresResult { get; set; }
        public IExchange traderCompra { get; set; }
        public IExchange traderVenda { get; set; }
    }
}