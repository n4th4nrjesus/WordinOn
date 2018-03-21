using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordinOn.WebUI.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro
        public ActionResult IndexProfessor()
        {
            return View();
        }

        public ActionResult IndexEstudante()
        {
            return View();
        }
    }
}