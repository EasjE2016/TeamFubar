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
    class KasserViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// fields som er private
        /// </summary>
        private BeregnPris _insertFinalSum;

        private BeregnPris _calculateFinalSum;
        private RelayCommand _calculateFinalSumCommand;

        public KasserViewModel()
        {
            _insertFinalSum = new BeregnPris();
            _calculateFinalSum = new BeregnPris();
           CalculateFinalSumCommand = new RelayCommand(CalculateFinalSum);      
        }


        public BeregnPris InsertFinalSum
        {
            get { return _insertFinalSum; }
            set { _insertFinalSum = value; }
        }

        public void CalculateFinalSum()
        {
            BeregnPris tempPris = new BeregnPris();

            tempPris.FinalSum = _insertFinalSum.FinalSum;
           
            tempPris.GetFinalSum();
            
        }

        public RelayCommand CalculateFinalSumCommand
        {
            get{ return _calculateFinalSumCommand; }
            set{ _calculateFinalSumCommand = value; }
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
