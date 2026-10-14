namespace Essenza.Application.Exceptions;

// Application informa que el recurso no existe; la API decidirá cómo responder por HTTP.
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
