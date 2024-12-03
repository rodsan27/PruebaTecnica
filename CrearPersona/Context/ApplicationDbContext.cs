using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrearPersona.Models;
using Microsoft.EntityFrameworkCore;

namespace CrearPersona.Context
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
