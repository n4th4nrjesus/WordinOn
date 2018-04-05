using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordinOn.DataAccess;
using WordinOn.Models;

namespace WordinOn.WebUI.Controllers
{
    public class EstudanteController : Controller
    {
        // GET: Estudante
        #region Tela Inicial
        public ActionResult TelaInicial()
        {
            return View();
        }

        public ActionResult Salas()
        {
            ViewBag.Sala = new SalaDAO().BuscarTodos();
            return View();
        }

        public ActionResult Redacao()
        {
            ViewBag.Redacao = new RedacaoDAO().BuscarTodos();
            return View();
        }

        #endregion

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

        public ActionResult Perfil()
        {
            return View();
        }

    }
}