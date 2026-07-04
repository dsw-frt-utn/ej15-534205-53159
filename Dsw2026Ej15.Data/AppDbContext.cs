using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain; 

namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer("Server=localhost;Database=DSW2026_Ej16;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}