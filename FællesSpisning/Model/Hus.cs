using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    public class Hus
    {
        public int AntalBørnU3 { get; set; }
        public int AntalBørn { get; set; }
        public int AntalUnge { get; set; }
        public int AntalVoksne { get; set; }
        public int HusNr { get; set; }
        public List<DateTime> DT { get; set; }

        public Hus()
        {

        }

        //Overloaded Constructor
        public Hus(int AntalBørnU3, int AntalBørn, int AntalUnge, int AntalVoksne, int HusNr)
        {
            this.AntalVoksne = AntalVoksne;
            this.AntalBørn = AntalBørn;
            this.AntalUnge = AntalUnge;
            this.AntalVoksne = AntalVoksne;
            this.HusNr = HusNr;
        }

        public override string ToString()
        {
            return $"Husnr: {HusNr}\r\nVoksne: {AntalVoksne} - Unge: {AntalUnge}\r\nBørn: {AntalBørn} - Børn U3: {AntalBørnU3}";
        }
    }
}