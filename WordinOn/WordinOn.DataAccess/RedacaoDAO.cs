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
        public void Inserir(Redacao obj, int codEstudante)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into Redacao (texto, tempo, codTema, codSala, codEstudante)
                                  values (@texto, @tempo, @codTema, @codSala, @codEstudante);";


                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = obj.Texto;
                    cmd.Parameters.Add("@tempo", SqlDbType.VarChar).Value = obj.Tempo;
                    cmd.Parameters.Add("@codTema", SqlDbType.Int).Value = obj.Tema.Cod;
                    cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = obj.Sala != null && obj.Sala.Cod > 0 ? obj.Sala.Cod : new Nullable<int>();
                    cmd.Parameters.Add("@codEstudante", SqlDbType.Int).Value = codEstudante;

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
        public List<Redacao> BuscarTodos()
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    r.*,
                                    u.nome as nome_estudante, 
                                    t.nome as Tema_Proposto
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
                            Data = Convert.ToDateTime(row["data"]),
                            Tempo = Convert.ToInt32(row["tempo"]),
                            Texto = row["texto"].ToString(),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["Tema_Proposto"].ToString(),
                            }
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
                                    r.*,
                                    u.nome as Nome_Pessoa, 
                                    t.nome as Tema_Proposto 
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
                            Texto = row["texto"].ToString(),
                            Tempo = Convert.ToInt32(row["tempo"]),
                            Data = Convert.ToDateTime(row["data"]),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["codEstudante"]),
                                Nome = row["Nome_Pessoa"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["codTema"]),
                                Nome = row["Tema_Proposto"].ToString()
                            }
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
	                                r.*, 
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
                            Tempo = Convert.ToInt32(row["Tempo"]),
                            Data = Convert.ToDateTime(row["data"]),
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["Nome_tema"].ToString(),
                                Descricao = row["Descricao_tema"].ToString()
                            },
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["codEstudante"]),
                                Nome = row["nome"].ToString()
                            }
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
	                                r.*, 
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
                            Tempo = Convert.ToInt32(row["Tempo"]),
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
                            }
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
                                                    r.*,
                                                    u.nome as nome_estudante,
                                                    t.nome as nome_tema,
                                                    s.nome as nome_sala
                                                from Redacao r
                                                inner join Usuario u on u.cod = r.codEstudante
                                                inner join Tema t on t.cod = r.codTema
                                                inner join Sala s on s.cod = r.codSala
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
                            Data = Convert.ToDateTime(row["data"]),
                            Tempo = Convert.ToInt32(row["tempo"]),
                            Texto = row["texto"].ToString(),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["codEstudante"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["codTema"]),
                                Nome = row["nome_tema"].ToString(),
                            },
                            Sala = new Sala()
                            {
                                Cod = Convert.ToInt32(row["codSala"]),
                                Nome = row["nome_sala"].ToString()
                            }
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
                                                    r.*,
                                                    u.nome as nome_estudante,
                                                    t.nome as nome_tema
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
                            Data = Convert.ToDateTime(row["data"]),
                            Tempo = Convert.ToInt32(row["tempo"]),
                            Texto = row["texto"].ToString(),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["codEstudante"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["codTema"]),
                                Nome = row["nome_tema"].ToString(),
                            }
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
                                        r.*,
                                        u.nome as Nome_Pessoa,
                                        t.descricao as Tema_Descricao, 
                                        t.nome as Tema_Proposto 
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
                        Data = Convert.ToDateTime(row["data"]),
                        Tempo = Convert.ToInt32(row["tempo"]),
                        Texto = row["texto"].ToString(),
                        Estudante = new Usuario()
                        {
                            Cod = Convert.ToInt32(row["codEstudante"]),
                            Nome = row["Nome_Pessoa"].ToString()
                        },
                        Tema = new Tema()
                        {
                            Cod = Convert.ToInt32(row["codTema"]),
                            Nome = row["Tema_Proposto"].ToString(),
                            Descricao = row["Tema_Descricao"].ToString()
                        }
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
                                                    r.*,
                                                    u.nome as nome_estudante,
                                                    t.nome as nome_tema,
                                                    s.cod as codSala,
                                                    s.nome as nome_sala
                                                from Redacao r
                                                inner join Usuario u on u.cod = r.codEstudante
                                                inner join Tema t on t.cod = r.codTema
                                                inner join Sala s on s.cod = r.codSala
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
                            Data = Convert.ToDateTime(row["data"]),
                            Tempo = Convert.ToInt32(row["tempo"]),
                            Texto = row["texto"].ToString(),
                            Estudante = new Usuario()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_estudante"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome_tema"].ToString(),
                            },
                            Sala = new Sala()
                            {
                                Cod = Convert.ToInt32(row["codSala"]),
                                Nome = row["nome_sala"].ToString()
                            }

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
