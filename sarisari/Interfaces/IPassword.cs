using System.Security;

namespace sarisari
{
    public interface IPassword
    {
        SecureString SecurePassword { get; }
    }
}
