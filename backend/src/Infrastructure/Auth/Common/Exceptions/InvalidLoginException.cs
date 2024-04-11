namespace ApplicationHandler.Infrastructure.Auth.Common.Exceptions;

public class InvalidLoginException : Exception
{
    public InvalidLoginException()
        : base("Invalid login or password") { }
}
