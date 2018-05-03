using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordinOn.Models
{
    public class Redacao
    {
        public int Cod { get; set; }
        public string Texto { get; set; }
        public int Tempo { get; set; }
        public Tema Tema { get; set; }
        public Usuario Estudante { get; set; }
        public DateTime Data { get; set; }
        public bool RAvaliada { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }

        public Redacao()
        {
            this.Avaliacoes = new List<Avaliacao>();
        }
    }
}
