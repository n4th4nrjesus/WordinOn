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
    public class SalaDAO
    {

        #region Inserir
        public void Inserir(Sala obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into Sala (nome)
                                              values (@nome);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        #endregion Inserir

        #region Buscar Todos
        public List<Sala> BuscarTodos()
        {
            var lst = new List<Sala>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"select nome from Sala";

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
                        var sala = new Sala()
                        {
                            Nome = row["nome"].ToString()

                        };
                        lst.Add(sala);
                    }
                }
            }
            return lst;
        }
        #endregion

        #region Procurar
        public List<Sala> Procurar(string texto)
        {
            var lst = new List<Sala>();

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = string.Format(@"select * from Sala where nome like '%{0}%';", texto); ;

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
                        var sala = new Sala()
                        {
                            Cod = Convert.ToInt32(row["cod"]),
                            Nome = row["nome"].ToString()
                        };
                        lst.Add(sala);
                    }
                }
            }
            return lst;
        }
        #endregion

    }
}
