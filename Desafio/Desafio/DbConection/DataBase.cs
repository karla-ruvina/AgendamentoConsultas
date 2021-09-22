using Desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Desafio.DbConection
{
    public class DataBase : DbContext
    {
        public DataBase() : base("Desafio")
        {

        }

        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Exames> Exames { get; set; }
        public DbSet<TiposExames> TiposExames { get; set; }
        public DbSet<Consultas> Consultas { get; set; }

    }
}