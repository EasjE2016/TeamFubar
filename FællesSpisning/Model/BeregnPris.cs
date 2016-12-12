using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FællesSpisning.Model
{
    class BeregnPris
    {
        /// <summary>
        /// Properties 
        /// </summary>
        public double FinalSum { get; set; }
        public double UdlagtSum { get; set; }
        public Hus HusObj { get; set; }




        public override string ToString()
        {
            return "Hus nummer: " + HusObj + "Endelig sum for ugen: " + FinalSum + "kr" + "."; 
        }


    }
}
