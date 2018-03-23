using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordinOn.WebUI.Controllers
{
    public class EstudanteController : Controller
    {
        // GET: Estudante
        public ActionResult TelaInicial()
        {
            return View();
        }

        public ActionResult CriarRedacao()
        {
            return View();
        }

        public ActionResult PropriasRedacoes()
        {
            return View();
        }

        public ActionResult AcessoRedacao()
        {
            return View();
        }

        public ActionResult AcessoRedacaoAvaliacao()
        {
            return View();
        }

        public ActionResult ListaSalas()
        {
            return View();
        }

        public ActionResult ListaRedacoesSala()
        {
            return View();
        }
    }
}