using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WordinOn.DataAccess;
using WordinOn.Models;

namespace WordinOn.WebUI.Controllers
{
    public class LoginController : Controller
    {
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

        public ActionResult Entrar(Usuario obj)
        {
            var usuarioLogado = new UsuarioDAO().Login(obj);

            if (usuarioLogado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var userData = new JavaScriptSerializer().Serialize(usuarioLogado);
            FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Email, userData, false);

            if (usuarioLogado.PerfilUsuario == Perfil.Estudante)
            {
                return RedirectToAction("TelaInicial", "Estudante");
            }

            else
            {
                return RedirectToAction("TelaInicial", "Professor");
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}