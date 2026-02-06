using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitApp.Models;
using FitApp.ViewModels;

namespace FitApp.ViewModels
{
    public partial class VnosViewModel : ViewModelBase
    {
        public event Action? ProfilShranjen;
        
       
        [ObservableProperty]
        private Spol spol = Models.Spol.Moski;
        [ObservableProperty]
        private int starost;
        [ObservableProperty]
        private double teza;
        [ObservableProperty]
        private double visina;
        [ObservableProperty]
        private double forma;

        [ObservableProperty]
        private string info = "";

        [ObservableProperty]
        private string errorMessage = "";




        [RelayCommand]
        private void Shrani()
        {
            

            ErrorMessage = "";
            if (Starost < 5 || Starost > 100)
            {
                ErrorMessage += "\nstarost mora biti me 5 in 100";
                Starost = 0;
            }
            if (Teza < 40 || Teza > 400)
            {
                ErrorMessage += "\nteža mora biti med 40 in 400 kg";
                Teza = 0;
            }
            if (Visina < 60 || Visina > 250)
            {
                ErrorMessage += "\nvišina mora biti med 60 in 250 cm";
                Visina = 0;
            }
            if (Starost != 0 && Teza != 0 && Visina != 0 )
            {
                Profil profil = new Profil(Spol,Starost,Teza,Visina,Forma);
                Profil.ShraniProfil(profil);
                ProfilShranjen?.Invoke();
                info = "Profil je bil uspešno shranjen";
                      
            }

            

            



        }
        




    }
}
