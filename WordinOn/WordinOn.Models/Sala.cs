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
        public int Quantidade { get; }
    }
}
