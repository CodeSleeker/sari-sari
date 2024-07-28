using System;
using System.ComponentModel.DataAnnotations.Schema;
using WpfCore;

namespace sarisari
{
    public class User : BaseViewModel
    {
        public int Id { get; set; }
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; OnPropertyChanged(); }
        }
        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; OnPropertyChanged(); }
        }
        private string _PIN;

        public string PIN
        {
            get { return _PIN; }
            set { _PIN = value; OnPropertyChanged(); }
        }
        public int? RoleId { get; set; }
        private Role _Role;

        public Role Role
        {
            get { return _Role; }
            set { _Role = value; OnPropertyChanged(); }
        }
        DateTime _DateAdded;
        public DateTime DateAdded { get { return _DateAdded; } set { _DateAdded = value; OnPropertyChanged(); } }
        DateTime _DateModified;
        public DateTime DateModified { get { return _DateModified; } set { _DateModified = value; OnPropertyChanged(); } }
    }
}
