using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class Hus
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

        public bool Equals(Hus obj)
        {
            return obj.DT.Equals(DT);
        }



        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Hus)obj);
        }

        public override string ToString()
        {
            return $"Husnr: {HusNr}\r\nVoksne: {AntalVoksne} - Unge: {AntalUnge}\r\nBørn: {AntalBørn} - Børn U3: {AntalBørnU3}";
        }
    }
}