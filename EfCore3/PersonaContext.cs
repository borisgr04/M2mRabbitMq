using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efcore
{
    public class PersonaContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=G:\2020\Upc2020\Microservices\blogging.db");
    }

    public class Persona
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
    }


}