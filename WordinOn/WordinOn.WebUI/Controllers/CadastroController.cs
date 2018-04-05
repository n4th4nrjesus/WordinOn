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
        #region Professor
        public ActionResult IndexProfessor()
        {
            return View();
        }

        public ActionResult SalvarProfessor(Usuario obj)
        {
            new UsuarioDAO().Inserir(obj);

            return RedirectToAction("IndexProfessor", "Cadastro");
        }

        #endregion

        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        #region Estudante
        public ActionResult IndexEstudante()
        {
            return View();
        }

        public ActionResult SalvarEstudante(Usuario obj)
        {
            new UsuarioDAO().Inserir(obj);

            return RedirectToAction("IndexEstudante", "Cadastro");
        }
        #endregion

    }
}