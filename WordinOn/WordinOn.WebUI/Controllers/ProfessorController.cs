﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordinOn.WebUI.Controllers
{
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult TelaInicial()
        {
            return View();
        }

        public ActionResult TelaAvaliacaoRedacao()
        {
            return View();
        }

        public ActionResult ListaSala()
        {
            return View();
        }

        public ActionResult RedacaoSala()
        {
            return View();
        }
    }
}