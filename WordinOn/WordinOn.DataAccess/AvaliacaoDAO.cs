﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class AvaliacaoDAO
    {
        #region Inserir
        public void Inserir(Avaliacao obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO Avaliacao (texto, valor, codProfessor, codRedacao)
                                  VALUES (@texto, @valor, @codProfessor, @codRedacao);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@texto", SqlDbType.VarChar).Value = obj.Texto;
                    cmd.Parameters.Add("@valor", SqlDbType.Int).Value = obj.Valor;
                    cmd.Parameters.Add("@codProfessor", SqlDbType.Int).Value = obj.Professor.Cod;
                    cmd.Parameters.Add("@codRedacao", SqlDbType.Int).Value = obj.Redacao.Cod;

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
        public List<Avaliacao> BuscarTodos()
        {
            var lst = new List<Avaliacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    a.*,
                                    u.nome as Nome_Professor
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
                                Cod = Convert.ToInt32(row["codProfessor"]),
                                Nome = row["Nome_Professor"].ToString()
                            },
                            Redacao = new Redacao()
                            {
                                Cod = Convert.ToInt32(row["codRedacao"])
                            }
                        };
                        lst.Add(avaliacao);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Buscar por Redação
        public List<Avaliacao> BuscarPorRedacao(int cod)
        {
            var lst = new List<Avaliacao>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    u.nome as Nome_Professor,
                                    a.*
                                    from Avaliacao a
                                    inner join Usuario u on u.cod = a.codProfessor
                                    where codRedacao = @codRedacao;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codRedacao", SqlDbType.Int).Value = cod;
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
                                Cod = Convert.ToInt32(row["codProfessor"]),
                                Nome = row["Nome_Professor"].ToString()
                            },
                            Redacao = new Redacao()
                            {
                                Cod = Convert.ToInt32(row["codRedacao"])
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
