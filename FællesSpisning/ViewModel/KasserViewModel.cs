using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FællesSpisning.Model;
using Windows.UI.Popups;


namespace FællesSpisning.ViewModel
{
    class KasserViewModel 
    {

        /// <summary>
        /// fields som er private
        /// </summary>
        private BeregnPris _insertFinalSum;

        private BeregnPris _calculateFinalSum;
        public RelayCommand CalculateFinalSumCommand { get; set; }

        public KasserViewModel()
        {
            _insertFinalSum = new BeregnPris();
            _calculateFinalSum = new BeregnPris();
           CalculateFinalSumCommand = new RelayCommand(CalculateFinalSum, null);      
        }


        public BeregnPris InsertFinalSum
        {
            get { return _insertFinalSum; }
            set { _insertFinalSum = value; }
        }

        public void CalculateFinalSum()
        {
            BeregnPris tempPris = new BeregnPris();
            tempPris.UdlagtSum1 = _insertFinalSum.UdlagtSum1;
            tempPris.UdlagtSum2 = _insertFinalSum.UdlagtSum2;
            tempPris.UdlagtSum3 = _insertFinalSum.UdlagtSum3;
            tempPris.UdlagtSum4 = _insertFinalSum.UdlagtSum4;
            tempPris.FinalSum = _insertFinalSum.FinalSum;
           
            tempPris.GetFinalSum();
            
        }


        ///// <summary>
        ///// Metode til at inserte Udlæg
        ///// </summary>

      


        /// <summary>
        /// Metode til at lægge udLæg sammen
        /// </summary>
        /// 



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
