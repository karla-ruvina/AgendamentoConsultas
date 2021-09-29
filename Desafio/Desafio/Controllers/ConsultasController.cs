using Desafio.DbConection;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class ConsultasController : Controller
    {
        private DataBase db = new DataBase();
        // GET: Consultas
        
        public ActionResult Cadastro(int? id)
        {
            Consultas consultas = new Consultas();

            //Select Tipo Exame
            var tipoexame = new List<SelectListItem>();
            tipoexame.Add(new SelectListItem { Text = "", Value = "" });
            tipoexame.AddRange(db.TiposExames.Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }));
            ViewBag.TipoExame = new SelectList(tipoexame, "Value", "Text");

            //Select Exame
            var exame = new List<SelectListItem>();
            exame.Add(new SelectListItem { Text = "", Value = "" });
            exame.AddRange(db.Exames.Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }));
            ViewBag.Exame = new SelectList(exame, "Value", "Text");

            //Select Paciente
            var paciente = new List<SelectListItem>();
            paciente.Add(new SelectListItem { Text = "", Value = "" });
            paciente.AddRange(db.Pacientes.Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }));
            ViewBag.Paciente = new SelectList(paciente, "Value", "Text");

            if (id.HasValue && id.Value > 0)
            {
                consultas = db.Consultas.FirstOrDefault(c => c.Id == id);
            }

            return View(consultas);
        }

        public ActionResult Salvar(Consultas model)
        {
            try
            {
                var obj = model.Id > 0 ? db.Consultas.SingleOrDefault(c => c.Id == model.Id) : new Consultas();
                obj.IdPaciente = model.IdPaciente;
                obj.IdExame = model.IdExame;
                obj.IdTipoExame = model.IdTipoExame;
                obj.Data = model.Data;

                if (obj.Id == 0)
                {
                    db.Consultas.Add(obj);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Cadastro", model);
            }
        }

        public JsonResult VerificarExame(int tipoexame = 0)
        {
            var exame = new List<SelectListItem>();
            exame.Add(new SelectListItem { Text = "", Value = "" });
            exame.AddRange(db.Exames.Where(c => c.IdTipoExame == tipoexame).Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }));
            ViewBag.TipoExame = new SelectList(exame, "Value", "Text");

            return Json(exame, JsonRequestBehavior.AllowGet);

        }

        public JsonResult VerificarAgenda(DateTime dataconsulta, int id = 0)
        {
            var horario = db.Consultas.FirstOrDefault(c => c.Data == dataconsulta && c.Id != id);

            if (horario != null)
            {
                var exists = true;

                return Json(exists, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var exists = false;
                return Json(exists, JsonRequestBehavior.AllowGet);

            }
        }

    }
}