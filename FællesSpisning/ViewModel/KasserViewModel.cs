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

        private Model.BeregnPrisListe _beregnPrisList;

        public RelayCommand CalculateFinalSumCommand { get; set; }
        public RelayCommand CalculateWeekHousePriceCommand { get; set; }


        public KasserViewModel()
        {
            _insertFinalSum = new BeregnPris();
           CalculateFinalSumCommand = new RelayCommand(CalculateFinalSum, null);

            _insertCalWeekHousePrice = new BeregnPris();
            CalculateWeekHousePriceCommand = new RelayCommand(CalculateWeekHousePrice, null);
            //_beregnPrisList = new Model.BeregnPrisListe();  
            
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


        public Model.BeregnPrisListe BeregnListe
        {
            get { return _beregnPrisList; }
            set { _beregnPrisList = value; }
        }

        private BeregnPris _insertCalWeekHousePrice;

        public BeregnPris InsertCalWeekHousePrice
        {
            get { return _insertCalWeekHousePrice; }
            set
            {
                _insertCalWeekHousePrice = value;
                OnPropertyChanged(nameof(InsertCalWeekHousePrice));
            }
        }

        private String _displayData2;

        public String DisplayData2
        {
            get { return _displayData2; }
            set { _displayData2 = value;
                OnPropertyChanged(nameof(DisplayData2)); }
        }


        public void CalculateWeekHousePrice()
        {
            BeregnPris tempPris2 = new BeregnPris();

            tempPris2.FinalSum = InsertCalWeekHousePrice.FinalSum;

            //tempPris2.WeekSumHouse = InsertCalWeekHousePrice.WeekSumHouse;
            
           // InsertFinalSum.FinalSum = tempPris2.GetWeekSumHouse();

            InsertCalWeekHousePrice.WeekSumHouse = tempPris2.GetWeekSumHouse();

            DisplayData2 = InsertCalWeekHousePrice.WeekSumHouse.ToString();


        }


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
