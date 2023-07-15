using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp6.Command;
using WpfApp6.Models;
using WpfApp6.View;

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
                OnPropertyChanged($"{nameof(SelectedCar)}");
            }
        }
        private CarClass? searchedCar;

        public CarClass? SearchedCar
        {
            get { return searchedCar; }
            set
            {
                searchedCar = value;
                OnPropertyChanged($"{nameof(SearchedCar)}");
            }
        }
        private CarClass? updatedCar;

        public CarClass? UpdatedCar
        {
            get { return updatedCar; }
            set
            {
                updatedCar = value;
                OnPropertyChanged($"{nameof(UpdatedCar)}");
            }
        }
        public ICommand? AddCommand { get; }
        public ICommand? DeleteCommand { get; }
        public ICommand? UpdateCommand { get; }
        public ICommand? ShowSearchedCommand { get; }
        public ICommand? ShowCarViewCommand { get; }
        public ICommand? ShowUpdatedWindowCommand { get; }

        private bool? canShowCar;

        public bool? CanShowCar
        {
            get { return canShowCar; }
            set
            {
                canShowCar = value;
                OnPropertyChanged($"{nameof(CanShowCar)}");
            }
        }

        

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewModelClass()
        {
            try
            {

                NewCar = new CarClass();
                Cars = new ObservableCollection<CarClass>();
                AddCommand = new RelayCommand(AddCar);
                selectedCar = new CarClass();
                updatedCar = new CarClass();
                UpdateCommand = new RelayCommand(UpdateSelectedCar);
                ShowSearchedCommand = new RelayCommand(ShowSearchedCar);
                ShowCarViewCommand = new RelayCommand(OpenShowCarWindow, CanOpenShowCarWindow);
                DeleteCommand = new RelayCommand(DeleteCar,CanDetedeCar);
                ShowUpdatedWindowCommand = new RelayCommand(ShowUpdatedWindow, CanShowUpdatedWindow);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        
        void ShowSearchedCar(object? parameter)
        {
            try
            {
                if (searchedID > Cars.Count) throw new IndexOutOfRangeException();

                SearchedCar = Cars[searchedID - 1];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        void OpenShowCarWindow(object? parameter)
        {
            ShowCarPropView ShowCarWindow = new ShowCarPropView();
            ShowCarWindow.DataContext = this;
            ShowCarWindow.ShowDialog();

        }
        bool CanOpenShowCarWindow(object? parameter)
        {
            if (selectedCar.Id != 0)
            {
                return true;
            }
            return false;
        }

        void AddCar(object? parameter)
        {
            try
            {
                if (!(NewCar.Year > 0 || NewCar.Year <= 0)) throw new Exception("Year must be numbers only");
                if (newCar.Make != "" && newCar.Model != "" && newCar.ImageUrl != "" && NewCar.Year != null)
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


        void DeleteCar(object? parameter)
        {
            try
            {
                Cars?.Remove(SelectedCar);
                selectedCar = new CarClass();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        bool CanDetedeCar(object? parameter)
        {
            if (selectedCar.Id != 0)
            {
                return true;
            }
            return false;
        }
        void ShowUpdatedWindow(object? parameter)
        {
            UpdateCarWindow updateCarWindow = new ();
            updateCarWindow.DataContext=this;
            updateCarWindow.ShowDialog();
        }
        bool CanShowUpdatedWindow(object? parameter)
        {
            if (selectedCar.Id != 0)
            {
                return true;
            }
            return false;
        }

        void UpdateSelectedCar(object? parameter)
        {
            updatedCar.Id = selectedCar.Id;
            selectedCar = updatedCar;
        }
    }

}
