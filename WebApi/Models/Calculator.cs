using Newtonsoft.Json;
using WebApi.Models.Requests;

namespace WebApi.Models
{
    public class Calculator
    {
        public IExchange TraderVenda { get; }
        public IExchange TraderCompra { get; }

        public Calculator(IExchange traderCompra, IExchange traderVenda)
        {
            TraderCompra = traderCompra;
            TraderVenda = traderVenda;
        }

        public CalcularValoresResult CalcularValores(CalcularValoresRequest request)
        {
            var vt = ValorFinal(request.ValorCompra, request.ValorVenda, request.Montante, TraderCompra.Percent, TraderVenda.Percent);
            var pl = PorcentagemLucro(vt, request.Montante);

            var resultadoFinal = new CalcularValoresResult
            {
                ValorTotal = vt,
                PorcentagemLucro = pl
            };

            return resultadoFinal;
        }

        private double ValorFinal(double valorCompra, double valorVenda, double montante, double porcentagemCompra, double porcentagemVenda)
        {
            var fee = 0.0007;
            var carteira = montante;

            carteira = carteira / valorCompra;
            carteira = carteira - (carteira * fee);

            carteira = carteira * valorVenda;

            var desconto = porcentagemCompra + porcentagemVenda;
            var valorLiquido = carteira - (carteira * desconto);

            var lucro = valorLiquido - montante;
            return lucro; 
            
        }

        private double PorcentagemLucro(double lucro, double montante)
        {
            return (100 * lucro) / montante;
        }

    }
}