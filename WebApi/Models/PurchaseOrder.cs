using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("TB_COMPRA")]
    public class PurchaseOrder
    {
        [Key]
        public int? Id { get; set; }
        public double ValorVenda { get; set; }
        public double ValorCompra { get; set; }
        public double Montante { get; set; }
        public int TraderCompra { get; set; }
        public int TraderVenda { get; set; }
        public double ValorTotal { get; set; }
        public double PorcentagemLucro { get; set; }
        
    }
}