using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();
        private readonly List<Speciality> _specialities = new List<Speciality>();

        public PersistenceInMemory()
        {
            
            try
            {
                string json = File.ReadAllText("doctors.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _doctors = JsonSerializer.Deserialize<List<Doctor>>(json, options) ?? new List<Doctor>();

              
                foreach (var doctor in _doctors)
                {
                    if (doctor.Speciality != null && !_specialities.Any(s => s.Id == doctor.Speciality.Id))
                    {
                        _specialities.Add(doctor.Speciality);
                    }
                }
            }
            catch (Exception)
            {
               
                _doctors = new List<Doctor>();
                _specialities = new List<Speciality>();
            }
        }

        public IEnumerable<Speciality> GetAllSpecialities()
        {
            return _specialities;
        }

        public IEnumerable<Doctor> GetActiveDoctors()
        {
            return _doctors.Where(d => d.IsActive).ToList();
        }

        public Doctor GetActiveDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public bool DeactivateDoctor(Guid id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
            if (doctor != null)
            {
                doctor.IsActive = false;
                return true; 
            }
            return false; 
        }
    }
}