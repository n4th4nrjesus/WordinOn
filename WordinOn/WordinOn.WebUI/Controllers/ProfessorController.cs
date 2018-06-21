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
            ViewBag.Salas = new SalaXProfessorDAO().ProcurarPorProf((((Usuario)User).Cod));
            return View();
        }

        public ActionResult AvaliacaoRedacao(int cod)
        {
            var obj = new RedacaoDAO().BuscarPorCod(cod);
            obj.Avaliacoes = new AvaliacaoDAO().BuscarPorRedacao(cod);
            return View(obj);
        }

        public ActionResult EnviarAvaliacao(Avaliacao obj)
        {
            var codProf = ((Usuario)User).Cod;
            new AvaliacaoDAO().Inserir(obj, codProf);
            return RedirectToAction("TelaInicial", "Professor");
        }

        public ActionResult ListaSalas()
        {
            ViewBag.Salas = new SalaXProfessorDAO().BuscarTexto(string.Empty, (((Usuario)User).Cod));
            return View();
        }

        public ActionResult BuscarSala(FiltroSalaViewModel obj)
        {
            ViewBag.Salas = new SalaXProfessorDAO().BuscarTexto(obj.CampoTexto, (((Usuario)User).Cod));
            return View("ListaSalas");
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

        public ActionResult CriarSala(int? cod)
        {
            if (cod.HasValue)
            {
                var obj = new SalaDAO().BuscarPorCod(cod.Value);
                obj.Professores = new SalaXProfessorDAO().BuscarPorSala(obj);
                obj.Estudantes = new SalaXEstudanteDAO().BuscarPorSala(obj);

                ViewBag.Professores = new UsuarioDAO().ProcurarProfessores(cod);
                ViewBag.Estudantes = new UsuarioDAO().ProcurarEstudantes(cod);

                return View(obj);
            }
            else
            {
                ViewBag.Professores = new UsuarioDAO().BuscarTodos();
                ViewBag.Estudantes = new UsuarioDAO().BuscarTodos();
            }

            return View();
        }

        public ActionResult InserirSala(Sala obj)
        {
            if (obj.Cod > 0)
            {
                new SalaDAO().Alterar(obj);
                return View("ListaSalas");
            }
            else
            {
                new SalaDAO().Inserir(obj);
            }
            return View();
        }

        [HttpPost]
        public ActionResult InserirProfessor(int codSala, int codProfessor)
        {
            var obj = new SalaXProfessor()
            {
                Sala = new Sala() { Cod = codSala },
                Professor = new Usuario() { Cod = codProfessor }
            };

            new SalaXProfessorDAO().Inserir(obj);

            var sala = new SalaDAO().BuscarPorCod(codSala);
            sala.Professores = new SalaXProfessorDAO().BuscarPorSala(sala);

            return PartialView("_Professores", sala);

        }

        [HttpPost]
        public ActionResult InserirEstudante(int codSala, int codEstudante)
        {
            var obj = new SalaXEstudante()
            {
                Sala = new Sala() { Cod = codSala },
                Estudante = new Usuario() { Cod = codEstudante }
            };

            new SalaXEstudanteDAO().Inserir(obj);

            var sala = new SalaDAO().BuscarPorCod(codSala);
            sala.Estudantes = new SalaXEstudanteDAO().BuscarPorSala(sala);

            return PartialView("_Estudantes", sala);
        }

        public ActionResult ListaTemas()
        {
            ViewBag.ListaTemaH = new TemaDAO().BuscarHabilitados();
            ViewBag.ListaTemaD = new TemaDAO().BuscarDesabilitados();

            return View();
        }

        public ActionResult ProcurarTema(FiltroRedacaoViewModel obj)
        {
            ViewBag.ListaTemaH = new TemaDAO().Procurar(obj.CampoTexto);
            ViewBag.ListaTemaD = new TemaDAO().Procurar(obj.CampoTexto);

            return View("ListaTemas");
        }

        public ActionResult DesabilitarTema(int cod)
        {
            new TemaDAO().Desabilitado(cod);

            ViewBag.ListaTemaH = new TemaDAO().BuscarHabilitados();
            ViewBag.ListaTemaD = new TemaDAO().BuscarDesabilitados();

            return View("ListaTemas");
        }

        public ActionResult HabilitarTema(int cod)
        {
            new TemaDAO().Habilitado(cod);

            ViewBag.ListaTemaH = new TemaDAO().BuscarHabilitados();
            ViewBag.ListaTemaD = new TemaDAO().BuscarDesabilitados();

            return View("ListaTemas");
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

        public ActionResult ProcurarRedacaoSala(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Redacoes = new RedacaoDAO().Procurar(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("TelaInicial", filtro);
        }

        public ActionResult ProcurarRedacao(FiltroRedacaoViewModel filtro)
        {
            ViewBag.Redacoes = new RedacaoDAO().Procurar(filtro.Sala != null ? filtro.Sala.Cod : new Nullable<int>(), filtro.RAvaliadas, filtro.CampoTexto);
            ViewBag.Salas = new SalaDAO().BuscarPorEstudante(((Usuario)User).Cod);
            return View("ListaRedacoesSala", filtro);
        }

        public void DeletarSala(SalaXProfessor obj)
        {
            new SalaXProfessorDAO().TirarDaSala(obj);
        }
    }
}