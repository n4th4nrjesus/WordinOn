using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordinOn.Models
{
    public class Nota
    {
        public int Cod { get; set; }
        public int Valor { get; set; }
        public Professor Professor { get; set; }
    }
}
