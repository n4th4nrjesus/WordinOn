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
    public class SalaXProfessorDAO
    {
        #region Inserir
        public void Inserir(SalaXProfessor obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"insert into salaXprofessor (codProfessor, codSala) 
                                    values (@codProfessor, @codSala);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codProfessor", SqlDbType.VarChar).Value = obj.Professor;
                    cmd.Parameters.Add("@codSala", SqlDbType.VarChar).Value = obj.Sala;

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
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
            {
                string strSQL = @"delete from SalaXProfessor where codProfessor = @codProfessor and codSala = @codSala";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@codProfessor", SqlDbType.VarChar).Value = obj.Professor;
                    cmd.Parameters.Add("@codSala", SqlDbType.VarChar).Value = obj.Sala;

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

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
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

            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=WordinOnDB;
                                                            Data Source=localhost;
                                                            Integrated Security=SSPI;"))
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

    }
}
