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
    }
}