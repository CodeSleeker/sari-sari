using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sarisari
{
    public class Supplier : WpfCore.BaseViewModel
    {
        public int Id { get; set; }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged(); }
        }

        private string _PhoneNumber;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; OnPropertyChanged(); }
        }

        private string _MobileNumber;

        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; OnPropertyChanged(); }
        }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        [NotMapped]
        public bool IsUpdating { get; set; }
    }
}
