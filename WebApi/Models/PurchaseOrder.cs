using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;

namespace WebApi.Models
{
    public class PurchaseOrder
    {   
        public int? Id { get; set; }
        public int? IdUsuario { get; set; }
        public double ValorVenda { get; set; }
        public double ValorCompra { get; set; }
        public double Montante { get; set; }
        public int TraderCompra { get; set; }
        public int TraderVenda { get; set; }
        public double ValorTotal { get; set; }
        public float PorcentagemLucro { get; set; }

        public static List<PurchaseOrder> BuscarOrdensDeCompra(int IdUsuario)
        {
            SqlConnection SQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            SQLConnection.Open();

            var sql = "select Id, IdUsuario, TraderCompra, ValorCompra, TraderVenda, ValorVenda, Montante, ValorTotal, PorcentagemLucro from compras where IdUsuario = @IdUsuario";
            SqlCommand cmd = SQLConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

            SqlDataReader sqlReader = cmd.ExecuteReader();

            List<PurchaseOrder> ordensDeCompra = new List<PurchaseOrder>();

            while(sqlReader.Read())
            {
                ordensDeCompra.Add(new PurchaseOrder
                {
                    Id = Convert.ToInt32(sqlReader["Id"]),
                    IdUsuario = Convert.ToInt32(sqlReader["IdUsuario"]),
                    ValorVenda = Convert.ToDouble(sqlReader["TraderCompra"]),
                    ValorCompra = Convert.ToDouble(sqlReader["ValorCompra"]),
                    Montante = Convert.ToDouble(sqlReader["TraderVenda"]),
                    TraderCompra = Convert.ToInt32(sqlReader["ValorVenda"]),
                    TraderVenda = Convert.ToInt32(sqlReader["Montante"]),
                    ValorTotal = Convert.ToDouble(sqlReader["ValorTotal"])
                });
            }

            SQLConnection.Close();

            return (ordensDeCompra);

        }

        public void Inserir()
        {
            SqlConnection SQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            SQLConnection.Open();
            var query = $"insert into Compras(IdUsuario, TraderCompra, ValorCompra, TraderVenda, ValorVenda, Montante, ValorTotal, PorcentagemLucro) values (@IdUsuario, @TraderCompra, @ValorCompra, @TraderVenda, @ValorVenda, @Montante, @ValorTotal, @PorcentagemLucro)";

            SqlCommand comandoInsert = SQLConnection.CreateCommand();
            comandoInsert.CommandType = CommandType.Text;
            comandoInsert.CommandText = query;

            comandoInsert.Parameters.AddWithValue("@IdUsuario", 1);
            comandoInsert.Parameters.AddWithValue("@TraderCompra", TraderCompra);
            comandoInsert.Parameters.AddWithValue("@ValorCompra", ValorCompra);
            comandoInsert.Parameters.AddWithValue("@TraderVenda", TraderVenda);
            comandoInsert.Parameters.AddWithValue("@ValorVenda", ValorVenda);
            comandoInsert.Parameters.AddWithValue("@Montante", Montante);
            comandoInsert.Parameters.AddWithValue("@ValorTotal", ValorTotal);
            comandoInsert.Parameters.AddWithValue("@PorcentagemLucro", PorcentagemLucro);

            comandoInsert.ExecuteNonQuery();
            SQLConnection.Close();
        }
        
    }
}