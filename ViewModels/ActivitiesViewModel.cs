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
        private List<string> sortKriteriji = new List<string> { "Naslov", "Vrsta", "Hitrost", "Pot", "Kalorije" };
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
            OsveziUI();
        }

        
        [RelayCommand]
        private void Sort() 
        {
            
            Aktivnosti = Aktivnost.PreberiVse();
            OsveziUI();
            switch (IzbranSort)
            {
                case "Naslov":
                    Aktivnost.InsertionSort(AktivnostiUI, (a, b) => string.Compare(a.Aktivnost.Naslov, b.Aktivnost.Naslov));
                    break;
                case "Vrsta":
                    Aktivnost.InsertionSort(AktivnostiUI, (a, b) => a.Aktivnost.Vrsta.CompareTo(b.Aktivnost.Vrsta));
                    break;
                case "Hitrost":
                    Aktivnost.InsertionSort(AktivnostiUI, (a, b) => b.Aktivnost.Hitrost.CompareTo(a.Aktivnost.Hitrost));
                    break;
                case "Pot":
                    Aktivnost.InsertionSort(AktivnostiUI, (a, b) => a.Aktivnost.Pot.CompareTo(b.Aktivnost.Pot));
                    break;
                case "Kalorije":
                    Aktivnost.InsertionSort(AktivnostiUI, (a, b) => b.Kalorije.CompareTo(a.Kalorije));
                    break;
                case null:
                    break;
                case "":
                    break;

               
            };

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
                OsveziUI();
            }

            
            VisinskaU = 0;
            PotU = 0;

            
            
        }

        [RelayCommand]
        private void RemoveSelected()
        {

            if(IzbranUI.Aktivnost != null)
            {
                Aktivnosti.Remove(IzbranUI.Aktivnost);
                Aktivnost.ShraniVse(Aktivnosti);
                SteviloAktivnosti = Aktivnosti.Count();
                OsveziUI();
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
        private ObservableCollection<AktivnostDisplay> aktivnostiUI = new();

        [ObservableProperty]
        private AktivnostDisplay? izbranUI;

        partial void OnIzbranUIChanged(AktivnostDisplay? value)
        {
            if (value == null)
            {
                VidnostPodrobnosti = false;
                return;
            }
            var profil = Profil.Preberi();
            if (profil == null)
            {
                VidnostPodrobnosti = false;
                return;
            }
            Cal = Izracuni.CAL(value.Aktivnost, profil);
            Calk = Izracuni.CALk(value.Aktivnost, profil);
            Tempo = Izracuni.Tempo(value.Aktivnost);
            Cas = value.Aktivnost.UreMinuteSekunde();
            VidnostPodrobnosti = true;
        }



        private void OsveziRangInMedalje()
        {
             
            // reset rangov
            foreach (var a in AktivnostiUI)
                a.Rang = 0;

            for (int r = 1; r <= 3; r++)
            {
                double max = double.MinValue;
                int index = -1;

                for (int i = 0; i < AktivnostiUI.Count; i++)
                {
                    // preskoči že rangirane
                    if (AktivnostiUI[i].Rang != 0)
                        continue;

                    if (AktivnostiUI[i].Kalorije > max)
                    {
                        max = AktivnostiUI[i].Kalorije;
                        index = i;
                    }
                }

                if (index != -1)
                    AktivnostiUI[index].Rang = r;
            }   
        }
        
        private void OsveziUI()
        {
            Aktivnosti = Aktivnost.PreberiVse();
            var profil = Profil.Preberi();

            
            AktivnostiUI.Clear();

            foreach (var a in Aktivnosti)
            {
                var display = new AktivnostDisplay();
                display.Aktivnost = a;

                if (profil == null)
                {
                    display.Kalorije = 0;
                }
                else
                {
                    display.Kalorije = Izracuni.CAL(a, profil);
                }
                display.Rang = 0;

                AktivnostiUI.Add(display);
            }
    
            OsveziRangInMedalje();
        }
    }
}
