using Desafio.DbConection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private DataBase db = new DataBase();
        public ActionResult Index()
        {
            ViewBag.Pacientes = db.Pacientes.Count();
            ViewBag.Exames = db.Exames.Count();
            ViewBag.Tipos = db.TiposExames.Count();
            ViewBag.Consultas = db.Consultas.Count();

            return View();
        }
    }
}