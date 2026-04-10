using Avalonia.Controls;
using Avalonia.Metadata;
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
        [ObservableProperty]
        private double skupneKalorije;
        [ObservableProperty]
        private double povprecnaHitrost;
        public StatisticsViewModel()
        {
            Osvezi();


        }
        
        
        [RelayCommand]
        public void Osvezi()
        {
        Aktivnosti = Aktivnost.PreberiVse();
        Profil profil = Profil.Preberi();
        SkupnaPot = Izracuni.SkupnaPot(Aktivnosti); // tvoja rekurzivna metoda   
        SkupneKalorije = Izracuni.SkupneKalorije(Aktivnosti, profil);   // Imena morajo biti z veliko da UI dobi podatek da so se spremenila
        PovprecnaHitrost = Izracuni.PovpHitrost(Aktivnosti);

        }
        
        
    }
}
