namespace Dsw2026Ej15.Domain;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
