namespace WebApi.Models.Requests
{
    public class Buy
    {
        public double ValorVenda { get; set; }
        public double ValorCompra { get; set; }
        public double Montante { get; set; }
        public double ValorTotal { get; set; }
        public double PorcentagemLucro { get; set; }
        public double TraderCompra { get; set; }
        public double TraderVenda { get; set; }
    }
}