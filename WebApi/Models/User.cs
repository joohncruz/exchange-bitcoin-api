using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApi.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static List<User> Buscar(string UserName, string Password)
        {
            SqlConnection SQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            SQLConnection.Open();

            var sql = "select Id, UserName, Email from Usuarios where UserName = @Username and Password = @Password";
            SqlCommand cmd = SQLConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@Username", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

            SqlDataReader sqlReader = cmd.ExecuteReader();

            List<User> users = new List<User>();

            while (sqlReader.Read())
            {
                users.Add(new User
                {
                    Id = Convert.ToInt32(sqlReader["Id"]),
                    UserName = sqlReader["UserName"].ToString(),
                    Email = sqlReader["Email"].ToString()
                });
            }
                
            SQLConnection.Close();

            return (users);

        }

        public void Inserir()
        {
            SqlConnection SQLConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            SQLConnection.Open();
            var query = $"insert into Usuarios(UserName, Email, Password) values (@UserName, @Email, @Password)";

            SqlCommand comandoInsert = SQLConnection.CreateCommand();
            comandoInsert.CommandType = CommandType.Text;
            comandoInsert.CommandText = query;

            comandoInsert.Parameters.AddWithValue("@UserName", UserName);
            comandoInsert.Parameters.AddWithValue("@Email", Email);
            comandoInsert.Parameters.AddWithValue("@Password", Password);

            comandoInsert.ExecuteNonQuery();
            SQLConnection.Close();
        }

    }
}