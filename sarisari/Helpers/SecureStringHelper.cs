using System;
using System.Runtime.InteropServices;
using System.Security;

namespace sarisari
{
    public static class SecureStringHelper
    {
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;
            var unmanageString = IntPtr.Zero;
            try
            {
                unmanageString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanageString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanageString);
            }
        }
    }
}
