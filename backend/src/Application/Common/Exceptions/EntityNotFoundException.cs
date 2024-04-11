namespace ApplicationHandler.Application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
        : base("Entity not found") { }
}
