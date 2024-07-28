using System.Security;

namespace sarisari
{
    public interface ICPassword
    {
        SecureString SecurePassword { get; }
        SecureString SecureCPassword { get; }
    }
}
