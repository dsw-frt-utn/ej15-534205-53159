using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Domain
{
    public interface IPersistence
    {
        IEnumerable<Speciality> GetAllSpecialities();
        IEnumerable<Doctor> GetActiveDoctors();
        Doctor GetActiveDoctorById(Guid id);
        bool DeactivateDoctor(Guid id);
    }
}

