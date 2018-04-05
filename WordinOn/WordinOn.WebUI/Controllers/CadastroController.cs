using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordinOn.DataAccess;
using WordinOn.Models;

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

        public ActionResult Salvar(Usuario obj)
        {
            new UsuarioDAO().Inserir(obj);

            return RedirectToAction("IndexEstudante", "Cadastro");
        }
    }
}