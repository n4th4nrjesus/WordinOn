using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class AvaliacaoDAO
    {
        #region Inserir
        public void Inserir(Avaliacao obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Avaliacao (texto, valor, codProfessor, codRedacao)
                                              values (@texto, @valor, @codProfessor, @codRedacao);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = obj.Texto;
                    cmd.Parameters.Add("@valor", SqlDbType.Int).Value = obj.Valor;
                    cmd.Parameters.Add("@codProfessor", SqlDbType.VarChar).Value = obj.Professor.Cod;
                    cmd.Parameters.Add("@codRedacao", SqlDbType.VarChar).Value = obj.Redacao.Cod;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Buscar Avaliacao
        public List<Avaliacao> BuscarAvaliacao(Usuario obj, Redacao obj2)
        {
            var lst = new List<Avaliacao>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select 
                                    u.nome as [Nome da Pessoa], 
                                    t.nome as [Tema Proposto], 
                                    r.data as Data
                                    from Redacao r 
                                    inner join Usuario u on u.cod = r.codEstudante
	                                inner join Tema t on t.cod = r.codTema 
                                    where r.cod = @rCod and u.Cod = @uCod;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Parameters.Add("@uCod", SqlDbType.VarChar).Value = obj.Cod;
                    cmd.Parameters.Add("@rCod", SqlDbType.VarChar).Value = obj2.Cod;
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var avaliacao = new Avaliacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Texto = row["texto"].ToString(),
                            Valor = Convert.ToInt32(row["valor"]),
                            Redacao = new Redacao()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Texto = row["texto"].ToString()
                            }
                        };
                        lst.Add(avaliacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Buscar Todos
        public List<Avaliacao> BuscarTodos()
        {
            var lst = new List<Avaliacao>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select 
                                    u.nome as [Nome do professor],
                                    a.texto as [Comentario]
                                    a.valor as [Valor]
                                    from Avaliacao a
                                    inner join Usuario u on u.cod = a.codProfessor";

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
                        var avaliacao = new Avaliacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Texto = row["texto"].ToString(),
                            Valor = Convert.ToInt32(row["valor"]),
                            Professor = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome"].ToString()
                            },
                            Redacao = new Redacao()
                            {
                                Cod = Convert.ToInt32(row["cod"])
                            }
                        };
                        lst.Add(avaliacao);
                    }
                }
            }
            return lst;
        }
        #endregion
    }
}
