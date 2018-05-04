using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordinOn.DataAccess;
using WordinOn.Models;
using WordinOn.WebUI.ViewModels;

namespace WordinOn.WebUI.Controllers
{
    [Authorize]
    public class EstudanteController : Controller
    {
        public ActionResult TelaInicial()
        {
            ViewBag.Redacoes = new RedacaoDAO().BuscarTodos();
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View();
        }

        public ActionResult CriarRedacao()
        {
            ViewBag.Salas = new SalaDAO().BuscarTodos();
            return View();
                //RedirectToAction("CriarRedacao", "EstudanteController");
        }

        public ActionResult PropriasRedacoes()
        {
            ViewBag.Redacoes = new RedacaoDAO().BuscarPropriasRedacoes(((Usuario)User).Cod);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View();
        }

        public ActionResult AcessoRedacao(int cod)
        {
            var obj = new RedacaoDAO().BuscarPorCod(cod);
            obj.Avaliacoes = new AvaliacaoDAO().BuscarPorRedacao(cod);
            if (obj.Avaliacoes.Count > 0)
                return RedirectToAction("AcessoRedacaoAvaliacao", "Estudante", new { @cod = cod });
            else
                return View(obj);
        }

        public ActionResult AcessoRedacaoAvaliacao(int cod)
        {
            var obj = new RedacaoDAO().BuscarPorCod(cod);
            obj.Avaliacoes = new AvaliacaoDAO().BuscarPorRedacao(cod);
            return View(obj);
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

        public ActionResult AlterarPerfil(Usuario obj)
        {

            new UsuarioDAO().Alterar((((Usuario)User).Cod), obj);
            return RedirectToAction("TelaInicial", "Estudante");
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

        public ActionResult Redacao()
        {
            ViewBag.Redacao = new RedacaoDAO().BuscarTodos();
            return View();
        }

        public ActionResult ProcurarRedacao(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Salas = new SalaDAO().BuscarTodos();
            ViewBag.Redacoes = new RedacaoDAO().Procurar(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("TelaInicial", filtro);
        }

        public ActionResult ProcurarPropriaRedacao(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Salas = new SalaDAO().BuscarTodos();
            ViewBag.Redacoes = new RedacaoDAO().ProcurarPropriaRedacao(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto, ((Usuario)User).Cod);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("PropriasRedacoes", filtro);
        }

        public ActionResult ProcurarSala(string texto)
        {
            ViewBag.Sala = new SalaDAO().Procurar(texto);
            return View();
        }

        public ActionResult ProcurarTema(string texto)
        {
            //ViewBag.Tema = new RedacaoDAO().Procurar(texto);
            return View();
        }

        public ActionResult EnviarRedacao(Redacao obj)
        {
            new RedacaoDAO().Inserir(obj);
            return RedirectToAction("TelaInicial", "Estudante");
        }

        public ActionResult Professores()
        {
            ViewBag.Sala = new UsuarioDAO().BuscarTodos();
            return View();
        }

        public ActionResult Avaliacao()
        {
            ViewBag.Sala = new AvaliacaoDAO().BuscarTodos();
            return View();
        }

        public ActionResult SairSala(SalaXEstudante obj)
        {
            new SalaXEstudanteDAO().TirarDaSala(obj);
            return View();
        }
    }
}