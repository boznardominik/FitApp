using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Avalonia.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace FitApp.Models
{
    public class Profil
    {
        public Spol Spol { get; set; } // true je moski
        public int Starost { get; set; }
        public double Teza { get; set; }
        public double Visina { get; set; }
        public double Forma { get; set; }

        private static string profilPot = "Profil.txt";

        public Profil()
        {

        }
        public Profil(Spol spol, int starost, double teza, double visina, double forma)
        {
            this.Spol = spol;
            this.Starost = starost;
            this.Teza = teza;
            this.Visina = visina;
            this.Forma = forma;

            
            
        }
        

        
        public override string ToString()
        {
            return $"{Spol};{Starost};{Teza};{Visina};{Forma}";
        }

        public static Profil IzVrstice(string vrstica)
        {
            var p = vrstica.Split(";");
            Profil profil = new Profil();

            
            profil.Spol = Enum.Parse<Spol>(p[0]);
            profil.Starost = int.Parse(p[1]);
            profil.Teza = double.Parse(p[2]);
            profil.Visina = double.Parse(p[3]);
            profil.Forma = double.Parse(p[4]);

            return profil;
        }
       
        public static bool ProfilObstaja()
        {
            
            if (File.Exists(profilPot))
            {
                try
                {
                    string vrsticaProfil = File.ReadAllLines(profilPot).First();
                    Profil profil = IzVrstice(vrsticaProfil);
                    return true;
                }
                catch
                {
                    return false;
                }
               
            }
            else
            {
                return false;
            }
              
        }
        public static void ShraniProfil(Profil profil)
        {
           // var vsebina = $"{Spol};{Starost};{Teza};{Visina};{Forma}";
            File.WriteAllText(profilPot, profil.ToString());          
        }
        public static Profil? Preberi()
        {


         
            if (!File.Exists(profilPot))
                return null;
            try
            {
                var vrstica = File.ReadAllLines(profilPot).First();
                return Profil.IzVrstice(vrstica);
            }
            catch
            {
                return null;
            }
        }
        public double kcalDan()
        {
            double a = 161;
            if (this.Spol == Spol.Moski)
            {
                a = 5;
            }
            return 10 * this.Teza + 6.25 * this.Visina - 5 * this.Starost + a;
        }

    }

    
}
