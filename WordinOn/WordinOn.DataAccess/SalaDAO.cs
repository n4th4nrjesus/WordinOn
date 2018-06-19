using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class SalaDAO
    {
        #region Inserir
        public void Inserir(Sala obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into Sala (nome) values (@nome);
                                  select SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;

                    conn.Open();
                    obj.Cod = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
        }
        #endregion Inserir

        #region alterar
        public void Alterar(Sala obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"update Sala set nome = @nome where cod = @cod;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@cod", SqlDbType.Int).Value = obj.Cod;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;

                    conn.Open();
                    obj.Cod = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
        }
        #endregion

        #region Buscar Todos
        public List<Sala> BuscarTodos()
        {
            var lst = new List<Sala>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select * from Sala";

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
                        var sala = new Sala()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Nome = row["nome"].ToString()
                        };
                        lst.Add(sala);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Procurar
        public List<Sala> Procurar(string texto, int codEstudante)
        {
            var lst = new List<Sala>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = string.Format(@"select * from Sala where nome like '%{0}%' and cod in (select codSala from salaXestudante where codEstudante = @codEstudante);", texto); ;

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

                    foreach (DataRow row in dt.Rows)
                    {
                        var sala = new Sala()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Nome = row["nome"].ToString()
                        };
                        lst.Add(sala);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region metodos pra carregar view bags
        public List<Sala> BuscarPorEstudante(int codEstudante)
        {
            var lst = new List<Sala>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select * from Sala where cod in (select codSala from salaXestudante where codEstudante = @codEstudante)";

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

                    foreach (DataRow row in dt.Rows)
                    {
                        var sala = new Sala()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Nome = row["nome"].ToString()
                        };
                        lst.Add(sala);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region buscar por codigo
        public Sala BuscarPorCod(int cod)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM SALA WHERE COD = @COD;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = cod;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var sala = new Sala()
                    {
                        Cod = Convert.ToInt32(row["cod"]),
                        Nome = row["nome"].ToString()
                    };

                    return sala;
                }
            }
        }
        #endregion

    }
}
