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
        public Tema Tema { get; set; }
        public Sala Sala { get; set; }
        public Usuario Estudante { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TimeSpan Duracao { get; set; }
        public bool RAvaliada { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }
        public List<Tema> Temas { get; set; }

        public Redacao()
        {
            this.Avaliacoes = new List<Avaliacao>();
            this.Temas = new List<Tema>();
        }
    }
}
