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
            var pl = PorcentagemLucro(vt, request.Montante, TraderCompra.Percent, TraderVenda.Percent);

            var resultadoFinal = new CalcularValoresResult
            {
                ValorTotal = vt,
                PorcentagemLucro = pl
            };

            var fullResult = new FullResult
            {
                calcularValoresRequest = request,
                calcularValoresResult = resultadoFinal,
                traderCompra = TraderCompra,
                traderVenda = TraderVenda
            };

            return resultadoFinal;
        }

        private double ValorFinal(double valorCompra, double valorVenda, double montante, double porcentagemCompra, double porcentagemVenda)
        {
            var montante2 = - (porcentagemCompra * montante) / 100;
            // valorVenda =- (porcentagemVenda * valorCompra) / 100;
            var montanteBTC = (montante2 / valorCompra);
            montanteBTC =- 0.0007;
            var valorVendaFinal = montanteBTC * valorVenda;
            valorVendaFinal =- (porcentagemVenda * valorVendaFinal) / 100;
            return montante2 - valorVendaFinal; 
        }

        private double PorcentagemLucro(double valorFinal, double montante, double porcentagemCompra, double porcentagemVenda)
        {
            var val1 = valorFinal - (valorFinal * (porcentagemCompra /100));
            var val2 = valorFinal - (porcentagemVenda * 100);
            return valorFinal - (valorFinal * (porcentagemVenda / 100));
        }

    }
}