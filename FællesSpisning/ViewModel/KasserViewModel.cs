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
        
        private BeregnPris _udlagtSum1;
        private BeregnPris _udlagtSum2;
        private BeregnPris _udlagtSum3;
        private BeregnPris _udlagtSum4;

        private BeregnPris _insertFinalSum;
        private RelayCommand _addFinalSumCommand;

        public KasserViewModel()
        {
            _udlagtSum1 = new BeregnPris();
            _udlagtSum2 = new BeregnPris();
            _udlagtSum3 = new BeregnPris();
            _udlagtSum4 = new BeregnPris();

            _insertFinalSum = new BeregnPris();
            _addFinalSumCommand = new RelayCommand(AddFinalSum);

        }

        public BeregnPris InsertFinalSum
        {
            get { return _insertFinalSum; }
            set { _insertFinalSum = value; }
        }

     

        public void AddFinalSum()
        {
            BeregnPris tempFinalSum = new BeregnPris();
            tempFinalSum.FinalSum = _insertFinalSum.FinalSum;
            
             
        }

        public RelayCommand AddFinalSumCommand
        {
            get{ return _addFinalSumCommand; }
            set{ _addFinalSumCommand = value; }
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
