using Desafio.DbConection;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class PacienteController : Controller
    {
        private DataBase db = new DataBase();
        // GET: Paciente
        public ActionResult Index()
        {
            List<Pacientes> pacientes = new List<Pacientes>();
            pacientes = db.Pacientes.ToList();
            return View();
        }
    }
}