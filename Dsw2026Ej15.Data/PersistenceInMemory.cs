using Dsw2026Ej15.Domain;
using System.Text.Json;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Doctor> _doctors = new();
    private readonly List<Speciality> _specialities = new();

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        try
        {
            string[] possiblePaths =
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json"),
                "specialities.json",
                Path.Combine(Directory.GetCurrentDirectory(), "specialities.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "specialities.json")
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var list = JsonSerializer.Deserialize<List<Speciality>>(json, options);
                    if (list != null && list.Count > 0)
                    {
                        _specialities.Clear();
                        _specialities.AddRange(list);
                        break;
                    }
                }
            }
        }
        catch
        {
            // Ignore if file cannot be read
        }
    }

    public IEnumerable<Speciality> GetAllSpecialities() => _specialities;

    public Speciality? GetSpecialityById(Guid id) => _specialities.FirstOrDefault(s => s.Id == id);

    public IEnumerable<Doctor> GetActiveDoctors() => _doctors.Where(d => d.IsActive).ToList();

    public Doctor? GetActiveDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);

    public Doctor AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
        return doctor;
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