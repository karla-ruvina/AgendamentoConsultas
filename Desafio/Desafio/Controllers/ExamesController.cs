using Desafio.DbConection;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class ExamesController : Controller
    {
        private DataBase db = new DataBase();
        // GET: Exames
        public ActionResult Index()
        {
            List<Exames> exames = new List<Exames>();
            exames = db.Exames.ToList();
            return View(exames);
        }
        public ActionResult Cadastro(int? id)
        {
            Exames exames = new Exames();

            //Select Tipo Exame
            var tipoexame = new List<SelectListItem>();
            tipoexame.Add(new SelectListItem { Text = "", Value = "" });
            tipoexame.AddRange(db.TiposExames.Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }));
            ViewBag.TipoExame = new SelectList(tipoexame, "Value", "Text");

            if(id.HasValue && id.Value > 0)
            {
                exames = db.Exames.FirstOrDefault(c => c.Id == id);
            }

            return View(exames);
        }

        public ActionResult Salvar(Exames model)
        {
            try
            {
                var obj = model.Id > 0 ? db.Exames.SingleOrDefault(c => c.Id == model.Id) : new Exames();
                obj.Nome = model.Nome;
                obj.Observacao = model.Observacao;
                obj.IdTipoExame = model.IdTipoExame;

                if (obj.Id == 0)
                {
                    db.Exames.Add(obj);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Cadastro", model);
            }
        }

        public ActionResult Remover(int? id)
        {
            try
            {
                Exames obj = db.Exames.FirstOrDefault(c => c.Id == id);
                db.Exames.Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Dispose();
                return RedirectToAction("Index", "Exames");
            }

            return RedirectToAction("Index", "Exames");
        }
    }
}