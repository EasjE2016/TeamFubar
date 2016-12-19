using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    public class Udlæg
    {
        //Unit test
        private double _udlagtSum;
        public double UdlagtSum
        {
            get { return _udlagtSum; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Udlagtsum 1 må ikke være negativ");
                }
                _udlagtSum = value;
            }
        }

        public DateTime DatoForUdlæg { get; set; }
        public int HusNr { get; set; }

        public Udlæg()
        {
        }

        public Udlæg(DateTime DatoForUdlæg, double UdlagtSum, int HusNr)
        {
            this.DatoForUdlæg = DatoForUdlæg;
            this.UdlagtSum = UdlagtSum;
            this.HusNr = HusNr;
        }

        public override string ToString()
        {
            return $"Husstand: {HusNr} udlagt: {UdlagtSum} kr. - {DatoForUdlæg.ToString("MM/dd")}";
        }
    }
}
