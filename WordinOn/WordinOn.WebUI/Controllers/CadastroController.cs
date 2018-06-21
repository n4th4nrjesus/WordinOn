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
        #region Professor
        public ActionResult IndexProfessor()
        {
            return View();
        }

        public ActionResult SalvarProfessor(Usuario obj)
        {
            obj.PerfilUsuario = Perfil.Professor;
            obj.Chave = Guid.NewGuid().ToString();

            if (!Validacoes.ValidarEmail(obj.Email))
            {
                ViewBag.ErroMsg = "E-mail inválido!";
                return View("IndexProfessor");
            }

            new UsuarioDAO().InserirProfessor(obj);

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
            obj.PerfilUsuario = Perfil.Estudante;

            if (!Validacoes.ValidarEmail(obj.Email))
            {
                ViewBag.ErroMsg = "E-mail inválido!";
                return View("IndexEstudante");
            }

            new UsuarioDAO().InserirEstudante(obj);

            return RedirectToAction("IndexEstudante", "Cadastro");
        }
        #endregion
    }
}