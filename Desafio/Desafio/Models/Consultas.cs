using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Desafio.Models
{
    public class Consultas
    {
        [Key]
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public virtual Pacientes Pacientes { get; set; }
        public int IdTipoExame { get; set; }
        public virtual TiposExames TiposExames { get; set; }
        public int IdExame { get; set; }
        public virtual Exames Exames { get; set; }
        public DateTime Data { get; set; }
        public string Protocolo { get; set; }

    }
}