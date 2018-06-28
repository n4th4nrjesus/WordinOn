using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class SalaXEstudanteDAO
    {
        #region Inserir
        public void Inserir(SalaXEstudante obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into SalaXEstudante (codEstudante, codSala) 
                                    values (@codEstudante, @codSala);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.Int).Value = obj.Estudante.Cod;
                    cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = obj.Sala.Cod;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Tirar da Sala
        public void TirarDaSala(int estudante, int sala)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"delete from SalaXEstudante where codEstudante = @codEstudante and codSala = @codSala";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.Int).Value = estudante;
                    cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = sala;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Buscar Todos
        public List<SalaXEstudante> BuscarTodos()
        {
            var lst = new List<SalaXEstudante>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    nome, 
                                    email 
                                    from salaXestudante 
                                    inner join Usuario on salaXestudante.codEstudante = Usuario.cod
                                    ";

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
                        var salaXestudante = new SalaXEstudante()
                        {
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome"].ToString(),
                                Email = row["email"].ToString()
                            }//,
                            //Sala = new Sala()
                            //{
                            //    Nome = row["nome"].ToString()
                            //}
                        };
                        lst.Add(salaXestudante);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Procurar
        public List<Usuario> Procurar(string obj)
        {
            var lst = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select nome, email from Usuario where nome like '%' + @texto + '%' or email = '%' + @texto + '%';";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = obj;
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var usuario = new Usuario()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Nome = row["nome"].ToString(),
                            Email = row["email"].ToString()
                        };
                        lst.Add(usuario);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Buscar por Sala do Estudante
        public List<SalaXEstudante> BuscarPorSala(Sala obj)
        {
            var lst = new List<SalaXEstudante>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                      SALAXESTUDANTE.*,
                                      USUARIO.NOME, 
                                      USUARIO.EMAIL ,
                                      SALA.NOME as NOME_SALA
                                  FROM SALAXESTUDANTE 
                                  INNER JOIN USUARIO ON (SALAXESTUDANTE.CODESTUDANTE = USUARIO.COD)
                                  INNER JOIN SALA ON (SALAXESTUDANTE.CODSALA = SALA.COD)
                                  WHERE CODSALA = @CODSALA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@CODSALA", SqlDbType.Int).Value = obj.Cod;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var salaXestudante = new SalaXEstudante()
                        {
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["CODESTUDANTE"]),
                                Nome = row["NOME"].ToString(),
                                Email = row["EMAIL"].ToString()
                            },
                            Sala = new Sala()
                            {
                                Cod = Convert.ToInt32(row["CODSALA"]),
                                Nome = row["NOME_SALA"].ToString()
                            }
                        };
                        lst.Add(salaXestudante);
                    }
                }
            }
            return lst;
        }
        #endregion
    }
}
