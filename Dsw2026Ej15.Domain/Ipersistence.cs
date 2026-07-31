namespace Dsw2026Ej15.Domain;

public interface IPersistence
{
    IEnumerable<Speciality> GetAllSpecialities();
    Speciality? GetSpecialityById(Guid id);
    IEnumerable<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    Doctor AddDoctor(Doctor doctor);
    bool DeactivateDoctor(Guid id);
}
