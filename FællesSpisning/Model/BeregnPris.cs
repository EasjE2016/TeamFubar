using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FællesSpisning.Model
{
    public class BeregnPris
    {
        /// <summary>
        /// Properties 
        /// </summary>
        /// 

        private double _udlagtSum1;
        private double _udlagtSum2;
        private double _udlagtSum3;
        private double _udlagtSum4;
        private double _finalSum;
        private double _weekSumHouse;
        

        public double UdlagtSum1
        {
            get { return _udlagtSum1; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Udlagtsum 1 må ikke være negativ");
                }
                _udlagtSum1 = value; }
        }

        public double UdlagtSum2
        {
            get { return _udlagtSum2; }
            set { _udlagtSum2 = value; }
        }

        public double UdlagtSum3
        {
            get { return _udlagtSum3; }
            set { _udlagtSum3 = value; }
        }

        public double UdlagtSum4
        {
            get { return _udlagtSum4; }
            set { _udlagtSum4 = value; }
        }

        public double FinalSum
        {

            get { return _finalSum; }
            set { _finalSum = value; }
        }

        public double WeekSumHouse
        {
            get { return _weekSumHouse; }
            set { _weekSumHouse = value; }
        }

        public Hus HusObj { get; set; }

        public BeregnPris()
        {
            
        }

        public BeregnPris( double UdlagtSum1, double UdlagtSum2, double UdlagtSum3, double UdlagtSum4)
        {

            this.UdlagtSum1 = UdlagtSum1;
            this.UdlagtSum2 = UdlagtSum2;
            this.UdlagtSum3 = UdlagtSum3;
            this.UdlagtSum4 = UdlagtSum4;
        }


        //public double GetFinalSum()
        //{
        //    FinalSum = UdlagtSum1 + UdlagtSum2 + UdlagtSum3 + UdlagtSum4;
        //    return FinalSum;
        //}

            public double GetKuvertPrisUge()
        {
            FinalSum = (UdlagtSum1 + UdlagtSum2 + UdlagtSum3 + UdlagtSum4) / ((40 * 1) + (30 * 0.5) + (25*0.25));
            return FinalSum;
        }

        public double GetWeekSumHouse()
        {
            WeekSumHouse = (FinalSum * 1 * 8) + (FinalSum * 0.5 * 4) + (FinalSum * 0.25 * 4);
            return WeekSumHouse;
        }

        //public double GetKuvertPrisUge()
        //{
        //    FinalSum = (UdlagtSum1 + UdlagtSum2 + UdlagtSum3 + UdlagtSum4) /
        // ((HusObj.AntalVoksneTotal * 1) + (HusObj.AntalUngeTotal * 0.5) + (HusObj.AntalBørnTotal * 0.25));
        //    return FinalSum;
        //}

        //    public double GetWeekSumHouse()
        //{
        //    WeekSumHouse = (FinalSum*1*HusObj.AntalVoksne) + (FinalSum*0.5*HusObj.AntalUnge) + (FinalSum*0.25*HusObj.AntalBørn);
        //    return WeekSumHouse;

        //}



        /// <summary>
        /// foreah loop eksempel
        /// </summary>
        /// <returns></returns>


        //            foreach (Hus h in Total)
        //            {
        //                Console.WriteLine($"Prisen for ugen er i boligen:{h.GetWeekSumHouse()} kr");
        //                
        //           }

        public override string ToString()
        {
            return  "Husnr:" + "Hustandspris for ugen:" + WeekSumHouse + ".";
        }


    }
}
