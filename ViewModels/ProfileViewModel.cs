using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitApp.Models;
using FitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitApp.ViewModels
{
    public partial class ProfileViewModel : ViewModelBase
    {

        [ObservableProperty]
        private Profil taProfil;

        [ObservableProperty]
        private double bpm;

        public ProfileViewModel()
        {
            OsveziProfil();
            bpm = Izracuni.BMR(taProfil);

        }

        [RelayCommand]
        private void OsveziProfil()
        {
            TaProfil = Profil.Preberi();   
        }

    }
}
