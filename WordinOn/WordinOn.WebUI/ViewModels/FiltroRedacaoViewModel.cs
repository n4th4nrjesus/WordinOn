using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordinOn.Models;

namespace WordinOn.WebUI.ViewModels
{
    public class FiltroRedacaoViewModel
    {
        public bool RAvaliadas { get; set; }
        public string CampoTexto { get; set; }
        public Sala Sala { get; set; }
    }
}