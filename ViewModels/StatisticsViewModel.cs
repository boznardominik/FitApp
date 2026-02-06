using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitApp.Models;
using FitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitApp.ViewModels
{
    public partial class StatisticsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Aktivnost> aktivnosti;
        [ObservableProperty]
        private double skupnaPot;
        public StatisticsViewModel()
        {
            Osvezi();


        }
        [RelayCommand]
    public void Osvezi()
    {
        Aktivnosti = Aktivnost.PreberiVse();
        SkupnaPot = Izracuni.SkupnaPot(Aktivnosti); // tvoja rekurzivna metoda
    }
        
        
    }
}
