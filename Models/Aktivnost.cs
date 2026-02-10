using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace FitApp.Models
{
    public class Aktivnost : IComparable<Aktivnost>
    {
        public string Naslov { get; set; } = "";

        public VrstaAktivnosti Vrsta{ get; set; }
        public double Pot { get; set; }
        public double Hitrost { get; set; }
        public double Visinska { get; set; }

        private static string aktivnostPot = "Aktivnost.txt";

        public Aktivnost()
        {
            this.Naslov = "neizbrano";
            this.Vrsta = VrstaAktivnosti.Hoja;
            this.Pot = 1000;
            this.Hitrost = 5;
            this.Visinska = 0;
        }


        public Aktivnost(string naslov, VrstaAktivnosti vrsta, double pot,double hitrost, double visinska)
        {
            if (naslov == "")
            {
                this.Naslov = vrsta.ToString();
            }
            else
            {
                this.Naslov = naslov;

            }
            this.Vrsta = vrsta;
            this.Pot = pot;
            this.Hitrost = hitrost;
            this.Visinska = visinska;
        }
        public Aktivnost(string vrstica)
        {
            string[] aktivnost = vrstica.Split(";");
            this.Naslov = aktivnost[0];
            this.Vrsta = Enum.Parse<VrstaAktivnosti>(aktivnost[1]);
            this.Pot = double.Parse(aktivnost[2]);
            this.Hitrost = double.Parse(aktivnost[3]);
            this.Visinska = double.Parse(aktivnost[4]);
        }
        public override string ToString()
        {
            return this.Naslov + ";" + this.Vrsta + ";" + this.Pot + ";" + this.Hitrost + ";" + this.Visinska;
        }

        public static Aktivnost izVrstice(string vrstica)
        {
            var p = vrstica.Split(";");
            Aktivnost aktivnost = new Aktivnost();

            aktivnost.Naslov = p[0];
            aktivnost.Vrsta = Enum.Parse<VrstaAktivnosti>(p[1]);
            aktivnost.Pot = double.Parse(p[2]);
            aktivnost.Hitrost = double.Parse(p[3]);
            aktivnost.Visinska = double.Parse(p[4]);

            return aktivnost;
        }
        public static bool AktivnostObstaja()
        {
            if (File.Exists(aktivnostPot))
            {
                try
                {
                    string vrsticaAktivnost = File.ReadAllLines(aktivnostPot).First();
                    Aktivnost aktivnosti = izVrstice(vrsticaAktivnost);
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
        public static void ShraniVse(ObservableCollection<Aktivnost> aktivnosti)
        {
            string[] vrstice = new string[aktivnosti.Count()];
            for(int i = 0; i < aktivnosti.Count();i++)
            {
                vrstice[i] = aktivnosti[i].ToString();
            }
            File.WriteAllLines(aktivnostPot,vrstice );
        }
        public static void ShraniEno(Aktivnost aktivnost)
        {
            File.AppendAllText(aktivnostPot, aktivnost.ToString());
        }
        public static ObservableCollection<Aktivnost> PreberiVse()
        {
            if(!File.Exists(aktivnostPot))
            {
                return new ObservableCollection<Aktivnost>();
            }
            try
            {
                string[] vrstice = File.ReadAllLines(aktivnostPot);
                ObservableCollection<Aktivnost> seznam = new ObservableCollection<Aktivnost>();
                foreach(var item in vrstice)
                {
                    seznam.Add(izVrstice(item));
                }
                return seznam;
            }
            catch
            {
                return new ObservableCollection<Aktivnost>();
            }
        }
        public int CompareTo(Aktivnost? other)
        {
            if (other is null)
            {
                return 1;
            }
            return this.Pot.CompareTo(other.Pot);      
        }
        public static void InsortionSort<T>(ObservableCollection<T> collection, Func<T,T,int> compare)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                var tmp = collection[i];
                for (int j = i; j > 0 && compare(collection[j - 1],tmp) > 0; j--)
                {
                    collection[j] = collection[j - 1];
                    collection[j - 1] = tmp;
                }
            }
        }


    }
}
