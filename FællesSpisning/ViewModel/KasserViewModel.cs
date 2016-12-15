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

        public RelayCommand CalculateFinalSumCommand { get; set; }

        public KasserViewModel()
        {
            _insertFinalSum = new BeregnPris();
           CalculateFinalSumCommand = new RelayCommand(CalculateFinalSum, null);      
        }


        public BeregnPris InsertFinalSum
        {
            get { return _insertFinalSum; }
            set { _insertFinalSum = value;
                OnPropertyChanged(nameof(InsertFinalSum));
            }
        }

        private String _displayData;

        public String DisplayData
        {
            get { return _displayData; }
            set { _displayData = value;
                OnPropertyChanged(nameof(DisplayData));
            }
        }


        public void CalculateFinalSum()
        {
            BeregnPris tempPris = new BeregnPris();
            tempPris.UdlagtSum1 = InsertFinalSum.UdlagtSum1;
            tempPris.UdlagtSum2 = InsertFinalSum.UdlagtSum2;
            tempPris.UdlagtSum3 = InsertFinalSum.UdlagtSum3;
            tempPris.UdlagtSum4 = InsertFinalSum.UdlagtSum4;

            InsertFinalSum.FinalSum = tempPris.GetKuvertPrisUge();

            DisplayData = InsertFinalSum.FinalSum.ToString();
            
            
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
