using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordinOn.Models
{
    public class Avaliacao
    {
        public int Cod { get; set; }
        public string Texto { get; set; }
        public decimal Valor { get; set; }
        public Usuario Professor { get; set; }
        public Redacao Redacao { get; set; }
    }
}
