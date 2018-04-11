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
        public ActionResult TelaInicial()
        {
            return View();
        }

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

        public ActionResult EnviarRedacao(Redacao obj)
        {
            new RedacaoDAO().Inserir(obj);
            return RedirectToAction("TelaInicial", "Estudante");
        }

        public void ContagemRegressiva()
        {
            TimeSpan TempoInicial = new TimeSpan(1, 30, 0);
            TimeSpan NovoTempo = new TimeSpan(0, 0, 0);
            TimeSpan TempoASubtrair = new TimeSpan(0, 0, 1);

            for(NovoTempo = TempoInicial)
        }

    }
}