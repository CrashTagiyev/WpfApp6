using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp6.Command;
using WpfApp6.Models;

namespace WpfApp6.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        private ObservableCollection<CarClass>? _cars;

        public ObservableCollection<CarClass>? Cars
        {
            get { return _cars; }
            set
            {
                if (_cars != value)
                {
                    _cars = value;
                    OnPropertyChanged(nameof(Cars));
                }
            }
        }
        private int searchedID;

        public int SearchedID
        {
            get { return searchedID; }
            set
            {
                searchedID = value; OnPropertyChanged(nameof(SearchedID));
            }
        }

        private CarClass? newCar;

        public CarClass? NewCar
        {
            get { return newCar; }
            set
            {
                newCar = value;
                OnPropertyChanged(nameof(newCar));
            }
        }

        private CarClass? selectedCar;

        public CarClass? SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged($"{nameof(selectedCar)}");
            }
        }


        public ICommand AddCommand { get; }
        public ICommand ShowSearchedCommand { get; }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewModelClass()
        {
            NewCar = new CarClass();
            Cars = new ObservableCollection<CarClass>();
            AddCommand = new RelayCommand(AddCar);
            selectedCar = new CarClass();
            ShowSearchedCommand = new RelayCommand(ShowSelectedCar);
        }
        void ShowSelectedCar(object? parameter)
        {
            try
            {
                if (searchedID > Cars.Count) throw new IndexOutOfRangeException();
                
                SelectedCar = Cars[searchedID - 1];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        void AddCar(object? parameter)
        {
            try
            {
                if (!(NewCar.Year > 0 || NewCar.Year <= 0)) throw new Exception("Year must be numbers only");
                if (newCar.Make != "" && newCar.Model != "" && NewCar.Year != null)
                {
                    newCar.Id = ++SID.StaticID;
                    Cars.Add(NewCar);
                    NewCar = new CarClass();
                }
                else throw new Exception("Fill the places");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        bool CanAddCar(object parameter)
        {
            return true;
        }
    }
}
