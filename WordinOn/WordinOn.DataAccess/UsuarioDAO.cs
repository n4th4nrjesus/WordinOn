using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WordinOn.Models;

namespace WordinOn.DataAccess
{
    public class UsuarioDAO
    {
        #region Inserir Professor
        public void InserirProfessor(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into Usuario (nome, sobrenome, senha, email, chave, perfil_usuario) 
                                  values (@nome, @sobrenome, @senha, @email, @chave, @perfil_usuario);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@sobrenome", SqlDbType.VarChar).Value = obj.Sobrenome;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@chave", SqlDbType.VarChar).Value = obj.Chave;
                    cmd.Parameters.Add("@perfil_usuario", SqlDbType.Int).Value = Convert.ToInt32(obj.PerfilUsuario);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Inserir Estudante
        public void InserirEstudante(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"insert into Usuario (nome, sobrenome, senha, email, perfil_usuario) 
                                    values (@nome, @sobrenome, @senha, @email, @perfil_usuario);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@sobrenome", SqlDbType.VarChar).Value = obj.Sobrenome;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@perfil_usuario", SqlDbType.Int).Value = Convert.ToInt32(obj.PerfilUsuario);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Alterar
        public void Alterar(int cod, Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"update Usuario set nome = @nome, sobrenome = @sobrenome, senha = @senha, email = @email where cod = @cod;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@cod", SqlDbType.VarChar).Value = cod;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@sobrenome", SqlDbType.VarChar).Value = obj.Sobrenome;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region login
        public Usuario Login(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select top 1 * from Usuario where email = @email and senha = @senha;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;

                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var usuario = new Usuario()
                    {
                        Cod = Convert.ToInt32(row["cod"]),
                        Nome = row["nome"].ToString(),
                        Sobrenome = row["sobrenome"].ToString(),
                        Senha = row["senha"].ToString(),
                        Email = row["email"].ToString(),
                        PerfilUsuario = (Perfil)Convert.ToInt32(row["perfil_usuario"])
                    };

                    return usuario;
                }
            }
        }
        #endregion

        #region Buscar Todos
        public List<Usuario> BuscarTodos()
        {
            var lst = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select nome, sobrenome, senha, email from Usuario";

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
                        var usuario = new Usuario()
                        {
                            Nome = row["nome"].ToString(),
                            Sobrenome = row["sobrenome"].ToString(),
                            Senha = row["senha"].ToString(),
                            Email = row["email"].ToString()
                        };
                        lst.Add(usuario);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Procurar por Estudante
        public List<Usuario> ProcurarEstudantes(int? cod)
        {
            var lst = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select cod, nome, email from Usuario where perfil_usuario = 1 and cod not in (select codEstudante from salaXestudante where codSala = @cod)";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod;
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

        #region Procurar por Professor
        public List<Usuario> ProcurarProfessores(int? cod)
        {
            var lst = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"select cod, nome, email from Usuario where perfil_usuario = 2 and cod not in (select codProfessor from salaXprofessor where codSala = @cod)";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod;
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
    }
}
