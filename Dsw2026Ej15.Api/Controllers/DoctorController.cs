using Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult CreateDoctor([FromBody] CreateDoctorDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("El nombre es requerido.");
        }

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("La matrícula es requerida.");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("La especialidad especificada no existe.");
        }

        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            LicenseNumber = request.LicenseNumber.Trim(),
            IsActive = true,
            Speciality = speciality
        };

        _persistence.AddDoctor(doctor);

        return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
    }

    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var doctors = _persistence.GetActiveDoctors();
        return Ok(doctors);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null)
        {
            return NotFound();
        }

        var response = new
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality?.Name ?? string.Empty
        };

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeactivateDoctor(Guid id)
    {
        bool success = _persistence.DeactivateDoctor(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}