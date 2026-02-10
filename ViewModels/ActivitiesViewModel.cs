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
    public partial class ActivitiesViewModel : ViewModelBase
    {
       
        [ObservableProperty]
        private ObservableCollection<Aktivnost> aktivnosti;

        [ObservableProperty]
        private int steviloAktivnosti;
        
        [ObservableProperty]
        private Aktivnost izbranaAktivnost;


        [ObservableProperty]
        private List<string> sortKriteriji = new List<string> { "Naslov", "Vrsta", "Hitrost", "Pot" };
        [ObservableProperty]
        private string izbranSort = "Vrsta";

        [ObservableProperty]
        private string naslovU = "";
        
        private VrstaAktivnosti vrstaU;
        public VrstaAktivnosti VrstaU
        {
            get => vrstaU;
            set
            {
                if (vrstaU == value) return;
                vrstaU = value;
                SpremeniSlider(value);

            }
        }
        [ObservableProperty]
        private double potU;
        [ObservableProperty]
        private double hitrostU;
        [ObservableProperty]
        private double hitrostMin = 1;
        [ObservableProperty]
        private double hitrostMax = 10;
        [ObservableProperty]
        private double visinskaU;

        public IEnumerable<VrstaAktivnosti> Vrste { get; } = Enum.GetValues<VrstaAktivnosti>();
             

        
        [ObservableProperty] private string errorMessage = "";
        

        public ActivitiesViewModel()
        {
            
            Aktivnosti = Aktivnost.PreberiVse();
            SteviloAktivnosti = Aktivnosti.Count();
        }

        
        [RelayCommand]
        private void Sort() 
        { 
            Aktivnosti = Aktivnost.PreberiVse();
            switch (IzbranSort)
            {
                case "Naslov":
                    Aktivnost.InsortionSort(Aktivnosti, (a, b) => string.Compare(a.Naslov, b.Naslov));
                    break;
                case "Vrsta":
                    Aktivnost.InsortionSort(Aktivnosti, (a, b) => a.Vrsta.CompareTo(b.Vrsta));
                    break;
                case "Hitrost":
                    Aktivnost.InsortionSort(Aktivnosti, (a, b) => a.Hitrost.CompareTo(b.Hitrost));
                    break;
                case "Pot":
                    Aktivnost.InsortionSort(Aktivnosti, (a, b) => a.Pot.CompareTo(b.Pot));
                    break;
               
            };


            
            foreach (var item in Aktivnosti)
                Console.WriteLine(item);

            Aktivnost.ShraniVse(Aktivnosti);

        }


        [RelayCommand]
        private void AddActivity()
        {
            ErrorMessage = "";
            

            if (VrstaU == null)
            {
                ErrorMessage += "izberi vrsto\n";
            }
            if(PotU == 0)
            {
                ErrorMessage += "zapiši pot v številkah\n";
               
            }
            if(PotU <= 0)
            {
                ErrorMessage += "pot mora biti pozitivna\n";
               
            }
           
            if(VisinskaU < 0)
            {
                ErrorMessage += "višinska razlika mora biti pizitivna\n";
            }
            if(ErrorMessage == "")
            {
                
                Aktivnost aktivnost = new Aktivnost(NaslovU, VrstaU, PotU, HitrostU, VisinskaU);
                Aktivnosti.Add(aktivnost);
                Aktivnost.ShraniVse(Aktivnosti);
                Aktivnosti = Aktivnost.PreberiVse();
                SteviloAktivnosti = Aktivnosti.Count();
            }

            
            VisinskaU = 0;
            PotU = 0;

            
            
        }

        [RelayCommand]
        private void RemoveSelected()
        {

            if(IzbranaAktivnost != null)
            {
                Aktivnosti.Remove(IzbranaAktivnost);
                Aktivnost.ShraniVse(Aktivnosti);
                SteviloAktivnosti = Aktivnosti.Count();
            }

        }

        private void SpremeniSlider(VrstaAktivnosti vrsta)
        {
            switch(vrsta)
            {
                case VrstaAktivnosti.Hoja:
                    HitrostMin = 1;
                    HitrostMax = 7;
                    HitrostU = 5;
                    break;
                case VrstaAktivnosti.Tek:
                    HitrostMin = 7;
                    HitrostMax = 45;
                    HitrostU = 10;
                    break;
                case VrstaAktivnosti.Kolesarjenje:
                    HitrostMin = 1;
                    HitrostMax = 60;
                    HitrostU = 20;
                    break;
                case VrstaAktivnosti.Plavanje:
                    HitrostMin = 1;
                    HitrostMax = 9;
                    HitrostU = 3;
                    break;


            }
        }


        [ObservableProperty]
        private double cal;
        [ObservableProperty]
        private double calk;
        [ObservableProperty]
        private double tempo;
        [ObservableProperty]
        private string cas;

        [ObservableProperty]
        private bool vidnostPodrobnosti;

        [ObservableProperty]
        private bool izbrano = false;

        [RelayCommand]
        private void Rezultati()
        {

            Izbrano = true;

            Profil profil = Profil.Preberi();
            
            if(IzbranaAktivnost == null || profil == null)
            {
                VidnostPodrobnosti = false;
                return;
            }

            Cal = Izracuni.CAL(IzbranaAktivnost, profil);
            Calk = Izracuni.CALk(IzbranaAktivnost, profil);
            Tempo = Izracuni.Tempo(IzbranaAktivnost);
            Cas = IzbranaAktivnost.UreMinuteSekunde();        // razsiritvena metoda
            if (IzbranaAktivnost == null || profil == null)
            {
                VidnostPodrobnosti = true;
            }
            VidnostPodrobnosti = true;
        }
        [ObservableProperty]
        private string dosezek = "fd";
        private void Dosezki()
        {
            Console.WriteLine("Dosezek");
        }



    }
}
