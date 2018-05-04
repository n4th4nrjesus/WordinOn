using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class RedacaoDAO
    {
        #region Inserir
        public void Inserir(Redacao obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into Redacao (texto, tempo, codTema, codAvaliacao, codNota, codEstudante, data)
                                              values (@texto, @tempo, @codTema, @codAvaliacao, @codNota, @codEstudante, @data);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = obj.Texto;
                    cmd.Parameters.Add("@tempo", SqlDbType.VarChar).Value = obj.Tempo;
                    cmd.Parameters.Add("@codTema", SqlDbType.Int).Value = obj.Tema.Cod;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.Int).Value = obj.Estudante.Cod;
                    cmd.Parameters.Add("@data", SqlDbType.VarChar).Value = obj.Data;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Buscar Todos
        public List<Redacao> BuscarTodos()
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    r.cod,
                                    u.nome as Nome_Pessoa, 
                                    t.nome as Tema_Proposto, 
                                    r.data as Data
                                    from Redacao r 
                                    inner join Usuario u on u.cod = r.codEstudante
	                                inner join Tema t on t.cod = r.codTema;";

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
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Estudante = new Usuario()
                            {
                                Nome = row["Nome_Pessoa"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Nome = row["Tema_Proposto"].ToString()
                            },
                            Data = Convert.ToDateTime(row["data"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Buscar Próprias Redações
        public List<Redacao> BuscarPropriasRedacoes(int obj)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    r.cod,
                                    u.nome as Nome_Pessoa, 
                                    t.nome as Tema_Proposto, 
                                    r.data as Data
                                    from Redacao r
                                    inner join Usuario u on u.cod = r.codEstudante
	                                inner join Tema t on t.cod = r.codTema
                                    where codEstudante = @codEstudante;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Parameters.Add("@codEstudante", SqlDbType.Int).Value = obj;
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Estudante = new Usuario()
                            {
                                Nome = row["Nome_Pessoa"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Nome = row["Tema_Proposto"].ToString()
                            },
                            Data = Convert.ToDateTime(row["data"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Acesso Redação
        public List<Redacao> AcessoRedacao(bool avaliadas)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
	                                r.texto as Texto_redacao,
	                                r.tempo as Tempo_redacao, 
	                                t.nome as Nome_tema, 
	                                t.descricao as Descricao_tema
	                                from Redacao r
	                                inner join Tema t on t.cod = r.codTema
                                    inner join Usuario u on u.cod = r.codEstudante
                                    where r.cod = @cod";

                if (avaliadas)
                {
                    strSQL += " and r.cod in (select codRedacao from Avaliacao);";
                }

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
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Texto = row["texto"].ToString(),
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome"].ToString(),
                                Descricao = row["descricao"].ToString()
                            },
                            Tempo = Convert.ToInt32(row["Tempo"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Acesso Redação Professor
        public List<Redacao> AcessoRedacaoProfessor(int obj)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
	                                r.texto,
	                                r.tempo, 
	                                t.nome,
	                                t.descricao,
                                    u.nome as [nome do estudante]
	                                from Redacao r
	                                inner join Tema t on t.cod = r.codTema
                                    inner join Usuario u on u.cod = r.codEstudante
                                    where r.cod = @cod";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Parameters.Add("@cod", SqlDbType.VarChar).Value = obj;
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);

                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Texto = row["texto"].ToString(),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["Tema Proposto"].ToString(),
                                Descricao = row["descricao"].ToString()
                            },
                            Tempo = Convert.ToInt32(row["Tempo"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Procurar
        public List<Redacao> Procurar(int? codSala, bool avaliadas, string texto)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = string.Format(@"select 
                                                    r.cod,
                                                    u.nome as nome_estudante,
                                                    t.nome as nome_tema,
                                                    r.data 
                                                from Redacao r
                                                inner join Usuario u on u.cod = r.codEstudante
                                                inner join Tema t on t.cod = r.codTema
                                                where (t.nome like '%{0}%' or u.nome like '%{0}%')", texto);

                if (codSala.HasValue && codSala.Value > 0)
                {
                    strSQL += " and r.codSala = @codSala ";
                }

                if (avaliadas)
                {
                    strSQL += " and r.cod in (select codRedacao from Avaliacao);";
                }

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;

                    if (codSala.HasValue && codSala.Value > 0)
                    {
                        cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = codSala;
                    }

                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_tema"].ToString()
                            },
                            Data = Convert.ToDateTime(row["data"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Procurar Própria Redação
        public List<Redacao> ProcurarPropriaRedacao(int? codSala, bool avaliadas, string texto, int codusuario)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = string.Format(@"select 
                                                    r.cod,
                                                    u.nome as nome_estudante,
                                                    t.nome as nome_tema,
                                                    r.data 
                                                from Redacao r
                                                inner join Usuario u on u.cod = r.codEstudante
                                                inner join Tema t on t.cod = r.codTema
                                                where (t.nome like '%{0}%' or u.nome like '%{0}%') and u.cod = @cod", texto);

                if (codSala.HasValue && codSala.Value > 0)
                {
                    strSQL += " and r.codSala = @codSala ";
                }

                if (avaliadas)
                {
                    strSQL += " and r.cod in (select codRedacao from Avaliacao);";
                }

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;

                    if (codSala.HasValue && codSala.Value > 0)
                    {
                        cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = codSala;
                    }
                    cmd.Parameters.Add("@cod", SqlDbType.Int).Value = codusuario;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_tema"].ToString()
                            },
                            Data = Convert.ToDateTime(row["data"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Buscar Por Cod
        public Redacao BuscarPorCod(int cod)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    r.cod,
                                    r.texto,
                                    u.nome as Nome_Pessoa, 
                                    t.nome as Tema_Proposto, 
                                    r.data as Data
                                    from Redacao r 
                                    inner join Usuario u on u.cod = r.codEstudante
	                                inner join Tema t on t.cod = r.codTema
                                where r.cod = @cod;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var redacao = new Redacao()
                    {
                        Cod = Convert.ToInt32(row["cod"]),
                        Estudante = new Usuario()
                        {
                            Nome = row["Nome_Pessoa"].ToString()
                        },
                        Tema = new Tema()
                        {
                            Nome = row["Tema_Proposto"].ToString()
                        },
                        Texto = row["texto"].ToString(),
                        Data = Convert.ToDateTime(row["data"])
                    };

                    return redacao;
                }
            }
        }
        #endregion

        #region Procurar pelo codigo da sala
        public List<Redacao> ProcurarPorSala(int? codSala)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = string.Format(@"select 
                                                    r.cod,
                                                    u.nome as nome_estudante,
                                                    t.nome as nome_tema,
                                                    r.data 
                                                from Redacao r
                                                inner join Usuario u on u.cod = r.codEstudante
                                                inner join Tema t on t.cod = r.codTema
                                                where r.codSala = @codSala");

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;

                    if (codSala.HasValue && codSala.Value > 0)
                    {
                        cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = codSala;
                    }

                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var redacao = new Redacao()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_tema"].ToString()
                            },
                            Data = Convert.ToDateTime(row["data"])
                        };
                        lst.Add(redacao);
                    }
                }
            }
            return lst;
        }
        #endregion
    }
}
