using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordinOn.DataAccess;
using WordinOn.Models;

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

        public ActionResult Login(Usuario obj)
        {
            var usuario = new UsuarioDAO().Login(obj.Email, obj.Senha);

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (usuario.PerfilUsuario == Perfil.Estudante)
            {
                return RedirectToAction("TelaInicial", "Estudante");
            }

            else
            {
                return RedirectToAction("TelaInicial", "Professor");
            }

        }
    }
}