using System;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

    
        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

   
        [HttpGet]
        public IActionResult GetActiveDoctors()
        {
            var doctors = _persistence.GetActiveDoctors();
            return Ok(doctors);
        }

    
        [HttpGet("{id}")]
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
                SpecialityName = doctor.Speciality?.Name
            };

            return Ok(response); 
        }

    
        [HttpDelete("{id}")]
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
}