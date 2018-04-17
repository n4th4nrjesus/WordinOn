using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordinOn.DataAccess;
using WordinOn.Models;

namespace WordinOn.WebUI.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult TelaInicial()
        {
            var lst = new RedacaoDAO().BuscarTodos();

            return View(lst);
        }

        public ActionResult TelaAvaliacaoRedacao()
        {
            return View();
        }

        public ActionResult ListaSalas()
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

        public ActionResult ListaTemas()
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

        public ActionResult Redacao()
        {
            ViewBag.Redacao = new RedacaoDAO().BuscarTodos();
            return View();
        }

        //VERIFICAR ESTE MÉTODO
        public ActionResult AvaliarRedacao(Redacao redacao, Avaliacao avaliacao)
        {
            new RedacaoDAO().AcessoRedacaoProfessor(redacao.Cod);
            new AvaliacaoDAO().Inserir(avaliacao);
            return View();
        }

        public ActionResult Salas()
        {
            ViewBag.Sala = new SalaDAO().BuscarTodos();
            return View();
        }

        public ActionResult Temas()
        {
            ViewBag.Temas = new TemaDAO().BuscarTodos();
            return View();
        }

        public ActionResult ProcurarRedacao(string texto)
        {
            ViewBag.Redacao = new RedacaoDAO().Procurar(texto);
            return View();
        }

        public ActionResult ProcurarSala(string texto)
        {
            ViewBag.Sala = new SalaDAO().Procurar(texto);
            return View();
        }

        public ActionResult ProcurarTema(string texto)
        {
            ViewBag.Tema = new RedacaoDAO().Procurar(texto);
            return View();
        }

        public ActionResult SalvarSala(Sala obj)
        {
            new SalaDAO().Inserir(obj);
            return RedirectToAction("ListaSala", "Professor");
        }

        public ActionResult CriarTema(Tema obj)
        {
            new TemaDAO().Inserir(obj);
            return RedirectToAction("ListaTema", "Professor");
        }

        public ActionResult PerfilProfessor(Usuario obj)
        {
            new UsuarioDAO().Alterar(obj);
            return RedirectToAction("TelaInicial", "Professor");
        }
    }
}