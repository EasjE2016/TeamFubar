using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FællesSpisning;
using System.ComponentModel;
using FællesSpisning.Model;
using Newtonsoft.Json;
using Windows.Storage;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

namespace FællesSpisning.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObsHusListeSingleton _instance;
        public ObsHusListeSingleton Instance
        {
            get { return _instance; }
            set { _instance = value;
                OnPropertyChanged(nameof(Instance));
            }
        }

        public ObservableCollection<Hus> HusListe { get; set; }

        //Nyt Kode
        //Relay Commands
        public RelayCommand AddEvent { get; set; }
        public RelayCommand DisplayEvent { get; set; }
        public RelayCommand RemoveEvent { get; set; }

        private DateTime _dateTime = DateTime.Today;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value.Date; OnPropertyChanged(nameof(DateTime)); }
        }


        private Hus _husTilListe;
        public Hus HusTilListe
        {
            get { return _husTilListe; }
            set { _husTilListe = value; OnPropertyChanged(nameof(HusTilListe)); }
        }

        private ObservableCollection<Hus> _tilmeldsListe;
        public ObservableCollection<Hus> TilmeldsListe
        {
            get { return _tilmeldsListe; }
            set { _tilmeldsListe = value;
                OnPropertyChanged(nameof(TilmeldsListe));
            }
        }

        private ObservableCollection<Hus> _result;
        public ObservableCollection<Hus> Result
        {
            get { return _result; }
            set { _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }


        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }


        private Hus _selectedHus;
        public Hus SelectedHus
        {
            get { return _selectedHus; }
            set { _selectedHus = value;
                OnPropertyChanged(nameof(SelectedHus));
            }
        }


        public MainViewModel()
        {
            this.Instance = ObsHusListeSingleton._Instance;
            HusListe = Instance.HusListe;

            AddEvent = new RelayCommand(AddEventOnDateTime, null);
            DisplayEvent = new RelayCommand(DisplayEventOnDateTime, null);
//            RemoveEvent = new RelayCommand(RemoveEventOnDateTime, null);

            HusTilListe = new Hus();
            //TilmeldsListe = new ObservableCollection<Hus>();
            //Result = new ObservableCollection<Hus>();
            TilmeldsListe = new ObservableCollection<Hus>();
            Result = new ObservableCollection<Hus>();
            SelectedHus = new Hus();

        }

        //public void RemoveEventOnDateTime()
        //{
        //    try
        //    {
        //        TilmeldsListe.Remove(TilmeldsListe.Where(x => x.DT == DateTime).Single());
        //    }
        //    catch (Exception)
        //    {

        //        MessageDialog noEvent = new MessageDialog("Ingen Begivenhed planlægt på dato");
        //        noEvent.Commands.Add(new UICommand { Label = "Ok" });
        //        noEvent.ShowAsync().AsTask();

        //    }
        //}

        public void AddEventOnDateTime()
        {
            HusListe[SelectedIndex].DT.Add(DateTime);
            SelectedHus = HusListe[SelectedIndex];

            //if (TilmeldsListe.Contains(new Hus { DT = DateTime }))
            //{
            //    MessageDialog eventAlreadyPresent = new MessageDialog("Allerede planlagt en begivenhed på denne dato");
            //    eventAlreadyPresent.Commands.Add(new UICommand { Label = "Ok" });
            //    eventAlreadyPresent.ShowAsync().AsTask();

            //}
            //else
            //{
                TilmeldsListe.Add(SelectedHus);
            //}

        }

        public void DisplayEventOnDateTime()
        {

            Result.Clear();
            
            try
            {
                foreach (Hus husObj in TilmeldsListe)
                {
                    foreach (DateTime DtHusObj in husObj.DT)
                    {
                        if(DtHusObj == DateTime)
                        {
                            if(!Result.Any(x => x.HusNr == husObj.HusNr))
                            {
                                Result.Add(husObj);
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

                MessageDialog noEvent = new MessageDialog("Ingen Begivenhed planlægt på dato");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();

            }
        }

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }


    }
}
