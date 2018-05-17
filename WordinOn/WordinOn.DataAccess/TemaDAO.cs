using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class TemaDAO
    {
        #region Inserir
        public void Inserir(Tema obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into Tema (nome, descricao) values (@nome, @descricao);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Buscar Todos
        public List<Tema> BuscarTodos()
        {
            var lst = new List<Tema>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select nome, descricao from Tema";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var tema = new Tema()
                        {
                            Nome = row["nome"].ToString(),
                            Descricao = row["descricao"].ToString()
                        };
                        lst.Add(tema);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Buscar Aleatoriamente
        public Tema BuscarAleatoriamente(int codEstudante)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select top 1 * from Tema where cod not in (select codTema from Redacao where codEstudante = @codEstudante) order by newid()";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.Int).Value = codEstudante;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var tema = new Tema()
                    {
                        Cod = Convert.ToInt32(row["cod"]),
                        Nome = row["nome"].ToString(),
                        Descricao = row["descricao"].ToString()
                    };

                    return tema;
                }
            }
        }
        #endregion

        #region Procurar
        public List<Tema> Procurar(string texto)
        {
            var lst = new List<Tema>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = string.Format(@"select nome, descricao, data from Tema where nome like '%{0}%';", texto); ;

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var tema = new Tema()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Nome = row["nome"].ToString()
                        };
                        lst.Add(tema);
                    }
                }
            }
            return lst;
        }
        #endregion
    }
}