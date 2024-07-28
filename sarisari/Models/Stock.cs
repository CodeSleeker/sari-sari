using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sarisari
{
    public class Stock:WpfCore.BaseViewModel
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }
        private Product _Product;
        public Product Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged(); }
        }

        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged(); }
        }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        [NotMapped]
        public bool IsUpdating { get; set; }
    }
}
