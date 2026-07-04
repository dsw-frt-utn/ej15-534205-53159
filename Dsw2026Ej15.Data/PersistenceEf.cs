using System;
using System.Collections.Generic;
using System.Linq;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEf()
        {
            _context = new AppDbContext();
        }

        
        public IEnumerable<Speciality> GetAllSpecialities()
        {
            return _context.Specialities.ToList();
        }

        
        public IEnumerable<Doctor> GetActiveDoctors()
        {
           
            return _context.Doctors.Where(d => d.IsActive).ToList();
        }

        
        public Doctor GetActiveDoctorById(Guid id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

       
        public bool DeactivateDoctor(Guid id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);

            if (doctor != null)
            {
                doctor.IsActive = false;

                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}