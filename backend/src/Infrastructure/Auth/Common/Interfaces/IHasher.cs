namespace ApplicationHandler.Infrastructure.Auth.Common.Interfaces;

public interface IHasher
{
    string Hash(string secret);
}
