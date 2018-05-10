using System;
using System.Web.Mvc;
using WordinOn.DataAccess;
using WordinOn.Models;
using WordinOn.WebUI.ViewModels;

namespace WordinOn.WebUI.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        
        public ActionResult TelaInicial()
        {
            ViewBag.Redacoes = new RedacaoDAO().BuscarTodos();
            ViewBag.Salas = new SalaDAO().BuscarTodos();
            return View();
        }

        public ActionResult AvaliacaoRedacao(int cod)
        {
            var obj = new RedacaoDAO().BuscarPorCod(cod);
            var aval = new Avaliacao();
            aval.Redacao = obj;
            return View(aval);
        }
        public ActionResult EnviarAvaliacao(Avaliacao obj)
        {
            var codProf = ((Usuario)User).Cod;
            
            new AvaliacaoDAO().Inserir(obj, codProf);
            return RedirectToAction("TelaInicial", "Professor");
        }

        public ActionResult ListaSalas()
        {
            var lst = new SalaDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult ListaRedacoesSala(int cod)
        {
            ViewBag.Redacoes = new RedacaoDAO().ProcurarPorSala(cod);
            return View();
        }

        public ActionResult CriarSala()
        {           
            ViewBag.Professores = new UsuarioDAO().ProcurarProfessores();
            return View();
        }

        #region Sala x Professor
        public ActionResult InserirProfessor(SalaXProfessor obj, int cod)
        {
            new SalaXProfessorDAO().Inserir(obj);
            return ViewBag.ProfessoresIn = new SalaXProfessorDAO().CarregarViewBag(cod);
        }
        #endregion

        public ActionResult InserirSala(Sala obj)
        {
            new SalaDAO().Inserir(obj);
            return RedirectToAction("ListaSalas", "Professor", new { @cod = obj.Cod });
        }

        public ActionResult ListaTemas()
        {
            var lst = new TemaDAO().BuscarTodos();

            return View(lst);
        }

        public ActionResult CriarTema()
        {
            return View();
        }

        public ActionResult SalvarTema(Tema obj)
        {
            new TemaDAO().Inserir(obj);
            return RedirectToAction("ListaTemas", "Professor");
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult AlterarPerfil(Usuario obj)
        {
            
            new UsuarioDAO().Alterar((((Usuario)User).Cod), obj);
            return RedirectToAction("TelaInicial", "Professor");
        }

        public ActionResult ProcurarRedacao(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Redacoes = new RedacaoDAO().Procurar(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto);
            return View("TelaInicial", filtro);
        }

        public ActionResult CriarSalaEstd()
        {
            ViewBag.Estudantes = new UsuarioDAO().ProcurarEstudantes();
            return View();
        }

        //A PARTIR DAQUI COMEÇAM OS MÉTODOS NÃO AUTOMÁTICOS, OU SEJA, QUE FORAM CIRADOS MANUALMENTE PORÉM NÃO UTILIZADOS
        #region Métodos não automáticos
        //public ActionResult Redacao()
        //{
        //    ViewBag.Redacao = new RedacaoDAO().BuscarTodos();
        //    return View();
        //}

        ////VERIFICAR ESTE MÉTODO
        //public ActionResult AvaliarRedacao(Redacao redacao, Avaliacao avaliacao)
        //{
        //    new RedacaoDAO().AcessoRedacaoProfessor(redacao.Cod);
        //    new AvaliacaoDAO().Inserir(avaliacao);
        //    return View();
        //}

        //public ActionResult ProcurarRedacao(string texto)
        //{
        //    ViewBag.Redacao = new RedacaoDAO().Procurar(texto);
        //    return View();
        //}

        //public ActionResult ProcurarSala(string texto)
        //{
        //    ViewBag.Sala = new SalaDAO().Procurar(texto);
        //    return View();
        //}

        //public ActionResult ProcurarTema(string texto)
        //{
        //    ViewBag.Tema = new RedacaoDAO().Procurar(texto);
        //    return View();
        //}

        //public ActionResult SalvarSala(Sala obj)
        //{
        //    new SalaDAO().Inserir(obj);
        //    return RedirectToAction("ListaSala", "Professor");
        //}

        //public ActionResult SalvarTema(Tema obj)
        //{
        //    new TemaDAO().Inserir(obj);
        //    return RedirectToAction("ListaTema", "Professor");
        //}

        ////public ActionResult PerfilProfessor(Usuario obj)
        ////{
        ////    //new UsuarioDAO().Alterar(obj);
        ////    //return RedirectToAction("TelaInicial", "Professor");
        ////}
        #endregion
    }
}