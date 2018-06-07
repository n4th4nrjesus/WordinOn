using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordinOn.Models;

namespace WordinOn.WebUI.ViewModels
{
	public class FiltroSalaViewModel
	{
		public string CampoTexto { get; set; }
		public Sala Sala { get; set; }
        public Sala Salas { get; set; }
    }
}