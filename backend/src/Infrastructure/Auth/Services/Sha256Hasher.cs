using System.Security.Cryptography;
using System.Text;

using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;

namespace ApplicationHandler.Infrastructure.Auth.Services;

public class Sha256Hasher : IHasher
{
    public string Hash(string secret)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(secret));

        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        return builder.ToString();

    }
}
