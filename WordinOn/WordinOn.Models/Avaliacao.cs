using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordinOn.Models
{
    public class Avaliacao
    {
        public int Cod { get; set; }
        public string Texto { get; set; }
        public string Valor{ get; set; }
        public Usuario Professor { get; set; }
        public Redacao Redacao{ get; set; }
    }
}
