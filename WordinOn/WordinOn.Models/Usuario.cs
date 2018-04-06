using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordinOn.Models
{
    public class Usuario
    {
        public int Cod { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Chave { get; set; }
        public Perfil PerfilUsuario { get; set; }
    }

    public enum Perfil
    {
        Estudante = 1,
        Professor = 2
    }
}
