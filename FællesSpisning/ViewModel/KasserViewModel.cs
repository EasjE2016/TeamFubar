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

        public BeregnPris UdlagtSum1
        {
            get { return _udlagtSum1; }
            set { _udlagtSum1 = value; }
        }

        private BeregnPris _udlagtSum2;

        public BeregnPris UdlagtSum2
        {
            get { return _udlagtSum2; }
            set { _udlagtSum2 = value; }
        }

        private BeregnPris _udlagtSum3;

        public BeregnPris UdlagtSum3
        {
            get { return _udlagtSum3; }
            set { _udlagtSum3 = value; }
        }

        private BeregnPris _udlagtSum4;

        public BeregnPris UdlagtSum4
        {
            get { return _udlagtSum4; }
            set { _udlagtSum4 = value; }
        }


        private BeregnPris _insertFinalSum;
        private RelayCommand _addFinalSumCommand;

        public KasserViewModel()
        {
            //_udlagtSum1 = new BeregnPris();
            //_udlagtSum2 = new BeregnPris();
            //_udlagtSum3 = new BeregnPris();
            //_udlagtSum4 = new BeregnPris();

            //_insertFinalSum = new BeregnPris( );

            _addFinalSumCommand = new RelayCommand(AddNewFinalSum);

        }

        //public BeregnPris InsertFinalSum
        //{
        //    get { return _insertFinalSum; }
        //    set { _insertFinalSum = value; }
        //}



        public void AddNewFinalSum()
        {
            BeregnPris tempFinalSum = new BeregnPris();
            //tempFinalSum.UdlagtSum1 = InsertFinalSum.UdlagtSum1;
            //tempFinalSum.UdlagtSum2 = _insertFinalSum.UdlagtSum2;
            //tempFinalSum.UdlagtSum3 = _insertFinalSum.UdlagtSum3;
            //tempFinalSum.UdlagtSum4 = _insertFinalSum.UdlagtSum4;
            //tempFinalSum.FinalSum = _insertFinalSum.FinalSum;

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
