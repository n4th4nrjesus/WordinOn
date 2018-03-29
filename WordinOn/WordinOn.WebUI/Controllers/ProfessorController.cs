using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordinOn.WebUI.Controllers
{
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult TelaInicial()
        {
            return View();
        }

        public ActionResult TelaAvaliacaoRedacao()
        {
            return View();
        }

        public ActionResult ListaSala()
        {
            return View();
        }

        public ActionResult RedacoesSala()
        {
            return View();
        }

        public ActionResult CriarSala()
        {
            return View();
        }

        public ActionResult ListaTema()
        {
            return View();
        }

        public ActionResult CriacaoTema()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }
    }
}