using System.Collections.Generic;
using System.Management;

namespace sarisari
{
    public class DeviceHelper
    {
        public static ManagementObjectSearcher mosCSProduct = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
        public static Dictionary<string, string> GetPair(ManagementObject mo)
        {
            var dict = new Dictionary<string, string>();
            foreach (var item in mo.Properties)
            {
                dict[item.Name.ToString()] = item.Value == null ? "" : item.Value.ToString();
            }
            return dict;
        }

        private static Dictionary<string, string> GetInfo(ManagementObjectSearcher mos)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                foreach (ManagementObject item in mos.Get())
                {
                    dict = GetPair(item);
                }
                return dict;
            }
            catch { return null; }
        }
        public static Dictionary<string, string> GetCSProduct()
        {
            return GetInfo(mosCSProduct);
        }
    }
}
