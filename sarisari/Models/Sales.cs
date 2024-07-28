namespace sarisari
{
    public class Sales : WpfCore.BaseViewModel
    {
        public int Id { get; set; }

        private string _OrderNo;
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; OnPropertyChanged(); }
        }

        private double _TotalAmount;
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

    }
}
