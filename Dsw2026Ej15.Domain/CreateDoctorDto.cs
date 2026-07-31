namespace Dsw2026Ej15.Domain;

public class CreateDoctorDto
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public Guid SpecialityId { get; set; }
}
