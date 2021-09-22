using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Desafio.Models
{
    public class Exames
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public int IdTipoExame { get; set; }
        public virtual TiposExames TiposExames { get; set; }
    }
}