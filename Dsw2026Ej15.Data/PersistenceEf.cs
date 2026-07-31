using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

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

    public Speciality? GetSpecialityById(Guid id)
    {
        return _context.Specialities.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Doctor> GetActiveDoctors()
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .Where(d => d.IsActive)
            .ToList();
    }

    public Doctor? GetActiveDoctorById(Guid id)
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == id && d.IsActive);
    }

    public Doctor AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
        return doctor;
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