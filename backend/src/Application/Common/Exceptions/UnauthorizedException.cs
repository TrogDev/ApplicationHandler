namespace ApplicationHandler.Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("Unauthorized") { }
}
