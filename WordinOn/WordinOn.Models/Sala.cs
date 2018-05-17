using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordinOn.Models
{
    public class Sala
    {
        public int Cod { get; set; }
        public string Nome { get; set; }
        public Usuario Professor { get; set; } // esta propriedade está aqui para que seja possível mostrasr qual professor criou a sala
        public int Quantidade { set; get; }

        public List<SalaXProfessor> Professores { get; set; }
        public List<SalaXEstudante> Estudantes { get; set; }

        public Sala()
        {
            this.Professores = new List<SalaXProfessor>();
            this.Estudantes = new List<SalaXEstudante>();
        }

        public Usuario Estudante { get; set; } // esta prorpie está aqui para que seja possível inserir estudantes na sala
    }
}
