using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public class Dsw2026Ej15DbContext : DbContext
    {
        public Dsw2026Ej15DbContext(DbContextOptions<Dsw2026Ej15DbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
    }
}