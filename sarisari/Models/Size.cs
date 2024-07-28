using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sarisari
{
    public class Size:WpfCore.BaseViewModel
    {
        public int Id { get; set; }
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Description { get; set; }
        public string Description { get { return _Description; } set { _Description = value; OnPropertyChanged(); } }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        [NotMapped]
        public bool IsUpdating { get; set; }
    }
}
