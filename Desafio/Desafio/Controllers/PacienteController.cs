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
            return View(pacientes);
        }

        public ActionResult Cadastro(int? id)
        {
            Pacientes pacientes = new Pacientes();

            //Select Tipo FornecedorCliente
            var sexo = new SelectList(new List<SelectListItem>
           {
                new SelectListItem { Text = "", Value = "" },
                new SelectListItem { Text = "Feminino", Value = "F" },
                new SelectListItem { Text = "Masculino", Value = "M" },
                new SelectListItem { Text = "-", Value = "-" }
           }, "Value", "Text", "");
            ViewBag.Sexo = sexo;


            if (id.HasValue && id.Value > 0)
            {
                var aux = db.Pacientes.FirstOrDefault(c => c.Id == id);
                pacientes.Id = aux.Id;
                pacientes.Nome = aux.Nome;
                pacientes.Email = aux.Email;
                pacientes.Sexo = aux.Sexo;
                pacientes.DataNascimento = aux.DataNascimento;
                pacientes.Cpf = aux.Cpf;
                pacientes.Telefone = aux.Telefone;
            }

            return View(pacientes);
        }

        [HttpPost]
        public JsonResult VerificarCPFExistente(string cpf, int id)
        {
            var user = db.Pacientes.FirstOrDefault(c => c.Cpf == cpf && c.Id != id);

            if (user != null)
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

        public ActionResult Salvar(Pacientes model)
        {
            try
            {
                var obj = model.Id > 0 ? db.Pacientes.SingleOrDefault(c => c.Id == model.Id) : new Pacientes();
                obj.Nome = model.Nome;
                obj.Email = model.Email;
                obj.Cpf = model.Cpf;
                obj.DataNascimento = model.DataNascimento;
                obj.Sexo = model.Sexo;
                obj.Telefone = model.Telefone;

                if (obj.Id == 0)
                {
                    db.Pacientes.Add(obj);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Cadastro", model);
            }
        }

        public ActionResult Remover(int? id)
        {
            try
            {
                Pacientes obj = db.Pacientes.FirstOrDefault(c => c.Id == id);
                db.Pacientes.Remove(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Dispose();
                return RedirectToAction("Index", "Paciente");
            }

            return RedirectToAction("Index", "Paciente");
        }
    }
}