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
        /// 

        private double _udlagtSum1;

        public double UdlagtSum1
        {
            get { return _udlagtSum1; }
            set { _udlagtSum1 = value; }
        }

        private double _udlagtSum2;

        public double UdlagtSum2
        {
            get { return _udlagtSum2; }
            set { _udlagtSum2 = value; }
        }

        private double _udlagtSum3;

        public double UdlagtSum3
        {
            get { return _udlagtSum3; }
            set { _udlagtSum3 = value; }
        }

        private double _udlagtSum4;

        public double UdlagtSum4
        {
            get { return _udlagtSum4; }
            set { _udlagtSum4 = value; }
        }

        private double _finalSum;

        public double FinalSum
        {

            get { return _finalSum; }
            set { _finalSum = value; }
        }

        public Hus HusObj { get; set; }

        public BeregnPris()
        {
           
        }


        public override string ToString()
        {
            return "kr" + ".";
        }


    }
}
