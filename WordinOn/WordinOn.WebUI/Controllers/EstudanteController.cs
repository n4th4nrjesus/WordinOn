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
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            var obj = new Redacao();
            obj.Tema = new TemaDAO().BuscarAleatoriamente(((Usuario)User).Cod);
            return View(obj);
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
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View();
        }

        public ActionResult ListaRedacoesSala(int cod)
        {
            ViewBag.Redacoes = new RedacaoDAO().ProcurarPorSala(cod);
            var filtro = new FiltroRedacaoViewModel()
            {
                Sala = new Sala() { Cod = cod }
            };
            return View(filtro);
        }

        public ActionResult Perfil()
        {
            var usuario = new UsuarioDAO().BuscarPorCod(((Usuario)User).Cod);
            return View(usuario);
        }

        public ActionResult AlterarPerfil(Usuario obj)
        {
            var isValid = Validacoes.ValidarEmail(obj.Email);

            if (!Validacoes.ValidarCampos(obj.Nome) || !Validacoes.ValidarCampos(obj.Sobrenome) || !Validacoes.ValidarCampos(obj.Senha) || !Validacoes.ValidarCampos(obj.Email))
            {
                ViewBag.ErroMsg = "Campos vazios não são permitidos!";
                return View("Perfil");
            }

            if (!isValid)
            {
                ViewBag.ErroMsg = "Email Inválido";
                return View("Perfil");
            }

            new UsuarioDAO().Alterar(((Usuario)User).Cod, obj);

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
            ViewBag.Redacoes = new RedacaoDAO().BuscarTodos();
            if (filtro.Sala.Cod != 0 || filtro.RAvaliadas != false || filtro.CampoTexto != null)
            {
                ViewBag.Redacoes = new RedacaoDAO().Procurar(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto);
            }
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("TelaInicial", filtro);
        }

        public ActionResult ProcurarSala(FiltroSalaViewModel filtro)
        {
            ViewBag.Salas = new SalaDAO().Procurar(filtro.CampoTexto, ((Usuario)User).Cod);
            return View("ListaSalas", filtro);
        }

        public ActionResult ProcurarRedacaoNaSala(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Redacoes = new RedacaoDAO().Procurar(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("ListaRedacoesSala", filtro);
        }

        public ActionResult ProcurarPropriaRedacao(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Redacoes = new RedacaoDAO().ProcurarPropriaRedacao(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto, ((Usuario)User).Cod);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("PropriasRedacoes", filtro);
        }

        public ActionResult ProcurarTema(string texto)
        {
            ViewBag.Tema = new TemaDAO().Procurar(texto);
            return View();
        }

        [HttpPost]
        public JsonResult EnviarRedacao(Redacao obj)
        {
            //timezone para o azure
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            //duração padrão de uma redação
            var duracaoTempo = new TimeSpan(01, 30, 00);

            //tempo que o estudando levou para fazer a redação
            obj.Duracao = duracaoTempo - obj.Duracao;

            obj.DataFim = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, zone);
            obj.DataInicio = obj.DataFim.Subtract(obj.Duracao);
            obj.Estudante = new Usuario() { Cod = ((Usuario)User).Cod };

            new RedacaoDAO().Inserir(obj);

            return Json(new
            {
                responseUrl = Url.Action("TelaInicial", "Estudante")
            });
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
    }
}