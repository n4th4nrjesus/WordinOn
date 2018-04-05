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
    public class SalaXEstudanteDAO
    {

        #region Inserir
        public void Inserir(SalaXEstudante obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Usuario (codEstudante, codSala) 
                                    values (@codEstudante, @codSala);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.VarChar).Value = obj.Estudante;
                    cmd.Parameters.Add("@codSala", SqlDbType.VarChar).Value = obj.Sala;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Excluir
        public void Excluir(SalaXEstudante obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Usuario (codEstudante, codSala) 
                                    values (@codEstudante, @codSala);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.VarChar).Value = obj.Estudante;
                    cmd.Parameters.Add("@codSala", SqlDbType.VarChar).Value = obj.Sala;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

        #region Sair da Sala
        public void Excluir(SalaXEstudante obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Usuario (codEstudante, codSala) 
                                    values (@codEstudante, @codSala);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codEstudante", SqlDbType.VarChar).Value = obj.Estudante;
                    cmd.Parameters.Add("@codSala", SqlDbType.VarChar).Value = obj.Sala;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion

    }
}
