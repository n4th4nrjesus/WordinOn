﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class SalaXProfessorDAO
    {
        #region Inserir
        public void Inserir(SalaXProfessor obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into salaXprofessor (codProfessor, codSala) 
                                    values (@codProfessor, @codSala);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codProfessor", SqlDbType.Int).Value = obj.Professor.Cod;
                    cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = obj.Sala.Cod;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Tirar da Sala
        public void TirarDaSala(SalaXProfessor obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"delete from SalaXProfessor where codProfessor = @codProfessor and codSala = @codSala";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codProfessor", SqlDbType.Int).Value = obj.Professor;
                    cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = obj.Sala;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Buscar Todos
        public List<SalaXProfessor> BuscarTodos()
        {
            var lst = new List<SalaXProfessor>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select 
                                    nome, 
                                    email 
                                    from SalaXProfessor 
                                    inner join Usuario on SalaXProfessor.codProfessor = Usuario.cod";

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
                        var SalaXProfessor = new SalaXProfessor()
                        {
                            Professor = new Usuario()
                            {
                                Nome = row["nome"].ToString(),
                                Email = row["email"].ToString()
                            },
                            Sala = new Sala()
                            {
                                Nome = row["nome"].ToString()
                            }
                        };
                        lst.Add(SalaXProfessor);
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

        #region Carregar ViewBag
        //public List<Usuario> CarregarViewBag(int cod)
        //{
        //    var lst = new List<Usuario>();

        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
        //    {
        //        string strSQL = @"select nome from Usuario where cod = @cod";

        //        using (SqlCommand cmd = new SqlCommand(strSQL))
        //        {
        //            conn.Open();
        //            cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod;
        //            cmd.Connection = conn;
        //            cmd.CommandText = strSQL;

        //            var dataReader = cmd.ExecuteReader();
        //            var dt = new DataTable();
        //            dt.Load(dataReader);

        //            conn.Close();

        //            foreach (DataRow row in dt.Rows)
        //            {
        //                var Professor = new Usuario()
        //                {
        //                    Cod = Convert.ToInt32(row["cod"]),
        //                    Nome = row["nome"].ToString(),
        //                };
        //                lst.Add(Professor);
        //            }
        //        }
        //    }
        //    return lst;
        //}
        #endregion

        public List<SalaXProfessor> BuscarPorSala(Sala obj)
        {
            var lst = new List<SalaXProfessor>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT 
                                      NOME, 
                                      EMAIL 
                                  FROM SALAXPROFESSOR 
                                  INNER JOIN USUARIO ON (SALAXPROFESSOR.CODPROFESSOR = USUARIO.COD)
                                  WHERE CODSALA = @CODSALA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codSala", SqlDbType.Int).Value = obj.Cod;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var salaXprofessor = new SalaXProfessor()
                        {
                            Professor = new Usuario()
                            {
                                Nome = row["nome"].ToString(),
                                Email = row["email"].ToString()
                            },
                            Sala = new Sala()
                            {
                                Nome = row["nome"].ToString()
                            }
                        };
                        lst.Add(salaXprofessor);
                    }
                }
            }
            return lst;
        }
    }
}
