using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Speciality> GetAllSpecialities()
        {
            return _context.Specialities.ToList();
        }

        public IEnumerable<Doctor> GetActiveDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToList();
        }

        public Doctor GetActiveDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public bool DeactivateDoctor(Guid id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
            if (doctor is null) return false;

            doctor.IsActive = false;
            _context.SaveChanges();
            return true;
        }
    }
}