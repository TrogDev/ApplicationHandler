namespace ApplicationHandler.Domain.Common;

public abstract class BaseEntity<KeyType>
{
    public KeyType Id { get; set; }
}
