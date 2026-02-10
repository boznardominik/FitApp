using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitApp.Models;
using System.Security;
namespace FitApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentViewModel;
        [ObservableProperty]
        private bool _vidnostNavigacij;

        VnosViewModel VnosVM { get; }
        ActivitiesViewModel ActivitiesVM { get; }

        ProfileViewModel ProfileVM { get; }
        
        StatisticsViewModel StatisticsVM { get; }

        public MainWindowViewModel()
        {
            VnosVM = new VnosViewModel();
            VnosVM.ProfilShranjen += OnProfilShranjeno;
            ActivitiesVM = new ActivitiesViewModel();
            ProfileVM = new ProfileViewModel();
            StatisticsVM = new StatisticsViewModel();

            if (Profil.ProfilObstaja())
            {
                _currentViewModel = ProfileVM;
                _vidnostNavigacij = true;
            }
            else
            {
                _currentViewModel = VnosVM;
                _vidnostNavigacij = false;
            }
            
                

        }

        [RelayCommand]
        public void PokaziVnos()
        {
            CurrentViewModel = VnosVM;
        }
        [RelayCommand]
        public void PokaziAktivnosti()
        {
            VidnostNavigacij = true;
            CurrentViewModel = ActivitiesVM;
            

        }
        [RelayCommand]
        public void PokaziProfil()
        {
            ProfileVM.OsveziProfil();

            CurrentViewModel = ProfileVM;
        }
        private void OnProfilShranjeno()
        {
            ProfileVM.OsveziProfil();
            VidnostNavigacij = true;
            CurrentViewModel = ProfileVM;  
        }


        [RelayCommand]
        public void PokaziStatistike()
        {
            CurrentViewModel = StatisticsVM;
            StatisticsVM.Osvezi();
        }


    }
}
