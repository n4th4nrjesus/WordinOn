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

        #region Tirar da Sala
        public void TirarDaSala(SalaXEstudante obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"delete from SalaXEstudante where codEstudante = @codEstudante and codSala = @codSala";

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

        #region Buscar Todos
        public List<SalaXEstudante> BuscarTodos()
        {
            var lst = new List<SalaXEstudante>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select 
                                    nome, 
                                    email 
                                    from salaXestudante 
                                    inner join Usuario on salaXestudante.codEstudante = Usuario.cod";

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
