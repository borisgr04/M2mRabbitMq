using System;
using System.Linq;

namespace efcore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PersonaContext())
            {
                // Create
                Console.WriteLine("Inserting a new Person");
                db.Add(new Persona { Nombre = " Fulanito de tal", Descripcion="Esta es un prueba" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a person");
                var person = db.Personas.First();

                
                var personas=db.Personas.ToList();
                foreach(var p in personas)
                {
                   Console.WriteLine($"{p.Nombre} {p.Descripcion}");     
                } 

            
            }
        }
    }


}
