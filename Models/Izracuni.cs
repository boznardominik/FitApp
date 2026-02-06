using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Models
{
    public static  class Izracuni
    {
        public static double MET(Aktivnost aktivnost)
        {
            var h = aktivnost.Hitrost;
            return aktivnost.Vrsta switch
            {
                VrstaAktivnosti.Hoja => h switch
                {
                    >= 6.4 => 6.0,
                    >= 5.6 => 5.3,
                    >= 4.0 => 4.8,
                    >= 3.0 => 2.5,
                    _ => 1.0

                },
                VrstaAktivnosti.Tek => h switch
                {
                    >= 19 => 20,
                    >= 16 => 17,
                    >= 14 => 15.5,
                    >= 12 => 12.5,
                    >= 11 => 10,
                    >= 6 => 8,
                    _ => 5
                },
                VrstaAktivnosti.Kolesarjenje => h switch
                {
                    >= 30 => 17,
                    >= 27 => 13,
                    >= 24 => 11,
                    >= 20 => 9,
                    >= 16 => 5,
                    _ => 3
                },
                VrstaAktivnosti.Plavanje => h switch
                {
                    >= 3.5 => 17,
                    >= 3 => 14,
                    >= 2.5 => 13,
                    >= 2 => 9,
                    >= 1 => 5,
                    _ => 4
                }

            };
            
        }
        public static double BMR(Profil profil)
        {
            if (profil == null) return 0;   

            double spolRazlika = 161;
            if (profil.Spol == Spol.Moski)
                spolRazlika = 5;

            return 10 * profil.Teza + 6.25 * profil.Visina - 5.0 * profil.Starost + spolRazlika;
        }
        public static double prilagojenMET(Aktivnost aktivnost,Profil profil)
        {
            double pMET = MET(aktivnost);

            double Grade = (aktivnost.Visinska / (aktivnost.Pot * 1000.0)) * 100.0;

            if (aktivnost.Visinska > 0)
            {
                 return aktivnost.Vrsta switch
                {
                    VrstaAktivnosti.Kolesarjenje => pMET = (0.2 * Grade) + pMET,
                    _ => pMET = (0.5 * Grade) + pMET
                };
            }
            return pMET;
        }
        
        public static double Cas(Aktivnost aktivnost) => aktivnost.Pot / aktivnost.Hitrost;

        public static double CasMin(this Aktivnost aktivnost) => Math.Round(Cas(aktivnost) * 60.0,2);

        public static double CAL(Aktivnost aktivnost, Profil profil)
        {
            double tezaM = profil.Teza - (profil.Teza * profil.Forma / 100);
            return Math.Round(prilagojenMET(aktivnost, profil) * tezaM * Cas(aktivnost), 2);
        }
        public static double CALk(Aktivnost aktivnost, Profil profil)
        {
            return Math.Round(CAL(aktivnost, profil) / aktivnost.Pot, 2);
        }
        public static double Tempo(Aktivnost aktivnost) => Math.Round(CasMin(aktivnost) / aktivnost.Pot,2);

        public static string UreMinuteSekunde(this Aktivnost aktivnost)
        {
            double Tsekunde = aktivnost.Pot / aktivnost.Hitrost *3600;

            int ure = (int)(Tsekunde / 3600);
            int minute = (int)(Tsekunde % 3600) / 60;
            int sekunde = (int)(Tsekunde % 60);

            return $"{ure:D2}:{minute:D2}:{sekunde:D2}";

        }
        public static double SkupnaPot(ObservableCollection<Aktivnost> aktivnosti,int a = 0)   // rekurzivna metoda za skupno pot aktivnosti
        {
            if (aktivnosti == null || aktivnosti.Count() == 0) 
            {
                return 0;
            }
            if (a >= aktivnosti.Count())
            {
                return 0;
            }
            return aktivnosti[a].Pot + SkupnaPot(aktivnosti, a + 1);
        }
        
            
        
    }
}
