using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordinOn.WebUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroProfessor()
        {
            return RedirectToAction("IndexProfessor", "Cadastro");
        }

        public ActionResult CadastroEstudante()
        {
            return RedirectToAction("IndexEstudante", "Cadastro");
        }
    }
}