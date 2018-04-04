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
    public class UsuarioDAO
    {

        #region Inserir
        public void Inserir(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
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
                    cmd.Parameters.Add("@perfil_usuario", SqlDbType.VarChar).Value = obj.PerfilUsuario;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion


        #region Alterar
        public void Alterar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"update Usuario set nome = @nome, sobrenome = @sobrenome, senha = @senha, email = @email, chave = @chave, perfil_usuario = @perfil_usuario where cod = @cod;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@cod", SqlDbType.VarChar).Value = obj.Cod;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@sobrenome", SqlDbType.VarChar).Value = obj.Sobrenome;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@chave", SqlDbType.VarChar).Value = obj.Chave;
                    cmd.Parameters.Add("@perfil_usuario", SqlDbType.VarChar).Value = obj.PerfilUsuario;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

    }
}
