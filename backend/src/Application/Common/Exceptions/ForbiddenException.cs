namespace ApplicationHandler.Application.Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("Forbidden") { }
}
