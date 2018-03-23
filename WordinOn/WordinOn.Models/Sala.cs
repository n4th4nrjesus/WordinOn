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
        public Professor Professor { get; set; }
        public Estudante Estudante { get; set; }
        public Redacao Redacao { get; set; }
        public int Quantidade { get; }
    }
}
