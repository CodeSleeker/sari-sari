namespace sarisari
{
    public class PurchaseProduct : WpfCore.BaseViewModel
    {
        public string Code { get; set; }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }

        private int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged(); }
        }

        private double _UnitPrice;

        public double UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; OnPropertyChanged(); }
        }

        private double _Discount;

        public double Discount
        {
            get { return _Discount; }
            set { _Discount = value; OnPropertyChanged(); }
        }

        private double _Total;

        public double Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged(); }
        }

    }
}
