﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class RedacaoDAO
    {

        #region Inserir
        public void Inserir(Redacao obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Redacao (texto, tempo, codTema, codAvaliacao, codNota, codEstudante, data)
                                              values (@texto, @tempo, @codTema, @codAvaliacao, @codNota, @codEstudante, @data);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = obj.Texto;
                    cmd.Parameters.Add("@tempo", SqlDbType.VarChar).Value = obj.Tempo;
                    cmd.Parameters.Add("@codTema", SqlDbType.VarChar).Value = obj.Tema.Cod;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.VarChar).Value = obj.Estudante.Cod;
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

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select 
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
        public List<Redacao> AcessoRedacao(int obj)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select 
	                                r.texto,
	                                r.tempo, 
	                                t.nome, 
	                                t.descricao	
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

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
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
        public List<Redacao> Procurar(string texto)
        {
            var lst = new List<Redacao>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = string.Format(@"select 
                                                    u.nome,
                                                    t.nome,
                                                    data 
                                                    from Redacao r
                                                    inner join Usuario u on u.cod = r.codEstudante
                                                    inner join Tema t on t.cod = r.codTema
                                                    where t.nome like '%{0}%';", texto); ;

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
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["Nome da Pessoa"].ToString()
                            },
                            Tema = new Tema()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["Tema Proposto"].ToString()
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
