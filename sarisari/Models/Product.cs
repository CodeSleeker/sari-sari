using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sarisari
{
    public class Product : WpfCore.BaseViewModel
    {
        public int Id { get; set; }

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }

        private string _Code;
        public string Code { get { return _Code; } set { _Code = value; OnPropertyChanged(); } }

        public int? CategoryId { get; set; }
        private Category _Category;
        public Category Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged(); }
        }

        public int? SupplierId { get; set; }
        private Supplier _Supplier;
        public Supplier Supplier
        {
            get { return _Supplier; }
            set { _Supplier = value; OnPropertyChanged(); }
        }

        public int? SizeId { get; set; }
        private Size _Size;
        public Size Size
        {
            get { return _Size; }
            set { _Size = value; OnPropertyChanged(); }
        }

        private string _Image;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; OnPropertyChanged(); }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged(); }
        }

        private double _ItemPurchasePrice;
        public double ItemPurchasePrice
        {
            get { return _ItemPurchasePrice; }
            set { _ItemPurchasePrice = value; OnPropertyChanged(); }
        }

        private double _SalesPrice;
        public double SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; OnPropertyChanged(); }
        }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        [NotMapped]
        public bool IsUpdating { get; set; }
    }
}
