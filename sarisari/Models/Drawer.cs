using System.ComponentModel.DataAnnotations.Schema;

namespace sarisari
{
    public class Drawer
    {
        public int Id { get; set; }
        public int DrawerId { get; set; }
        public string DeviceUUID { get; set; }
        public int? CashierId { get; set; }
        public User Cashier { get; set; }
    }
}
