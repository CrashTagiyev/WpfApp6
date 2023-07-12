using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Models
{
    public static class SID
    {
         static public int StaticID { get; set; }
    }
    public class CarClass : EntityClass, INotifyPropertyChanged
    {

        private string? make;
        public string? Make
        {
            get { return make; }
            set
            {
                make = value;
                OnPropertyChanged(nameof(Make));
            }
        }

        private string? model;
        public string? Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
        private int? year;
        public int? Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged($"{nameof(Year)}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
