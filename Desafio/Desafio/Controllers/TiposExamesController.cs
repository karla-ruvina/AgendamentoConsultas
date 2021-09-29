using Desafio.DbConection;
using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio.Controllers
{
    public class TiposExamesController : Controller
    {
        // GET: TiposExames
        private DataBase db = new DataBase();
        public ActionResult Index()
        {
            List<TiposExames> tipoexame = new List<TiposExames>();
            tipoexame = db.TiposExames.ToList();
            return View(tipoexame);
        }

        public ActionResult Cadastro(int? id)
        {
            TiposExames tipoexame = new TiposExames();

            if (id.HasValue && id.Value > 0)
            {
                tipoexame = db.TiposExames.FirstOrDefault(c => c.Id == id);
            }

            return View(tipoexame);
        }

        public ActionResult Salvar(TiposExames model)
        {
            try
            {
                var obj = model.Id > 0 ? db.TiposExames.SingleOrDefault(c => c.Id == model.Id) : new TiposExames();
                obj.Nome = model.Nome;
                obj.Descricao = model.Descricao;

                if (obj.Id == 0)
                {
                    db.TiposExames.Add(obj);
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
                TiposExames obj = db.TiposExames.FirstOrDefault(c => c.Id == id);
                db.TiposExames.Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Dispose();
                return RedirectToAction("Index", "TiposExames");
            }

            return RedirectToAction("Index", "TiposExames");
        }

    }
}