using System;
using System.ComponentModel.DataAnnotations.Schema;
using WpfCore;

namespace sarisari
{
    public class Role : BaseViewModel
    {
        public int Id { get; set; }
        string _Name;
        public string Name { get { return _Name; } set { _Name = value;OnPropertyChanged(); } }
        string _Description;
        public string Description { get { return _Description; } set { _Description = value; OnPropertyChanged(); } }
        DateTime _DateAdded;
        public DateTime DateAdded { get { return _DateAdded; } set { _DateAdded = value; OnPropertyChanged(); } }
        DateTime _DateModified;
        public DateTime DateModified { get { return _DateModified; } set { _DateModified = value; OnPropertyChanged(); } }
    }
}
