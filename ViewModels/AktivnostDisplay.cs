using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using FitApp.Models;

namespace FitApp.ViewModels
{
    public class AktivnostDisplay
    {
        public Aktivnost Aktivnost { get; set; } = null;
        public double Kalorije { get; set; }
        public int Rang { get; set; }
        public bool ImaMedaljo => Rang is 1 or 2 or 3;

        public string Medalja => Rang switch
        {
            1 => "🥇",
            2 => "🥈",
            3 => "🥉",
            _ => ""
        };
        public string MedaljaBarva => Rang switch
        {
            1 => "Gold",
            2 => "Silver",
            3 => "#CD7F32",
            _ => "Trasparent"
        };

    }

}
