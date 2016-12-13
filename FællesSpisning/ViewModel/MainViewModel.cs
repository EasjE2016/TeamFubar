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

        //Singleton
        private ObservableCollection<Hus> _husListe;
        public ObservableCollection<Hus> HusListe
        {
            get { return _husListe; }
            set { _husListe = value;
                OnPropertyChanged(nameof(HusListe));
            }
        }


        //Relay Commands
        public RelayCommand AddEvent { get; set; }
        public RelayCommand DisplayEvent { get; set; }
        public RelayCommand RemoveEvent { get; set; }
        public RelayCommand RemoveHouse { get; set; }

        //Konstant filnavn
        const String FileNameForTilmeldsListe = "saveTilmeldsListe.json";
        const String FileNameForHusListe = "saveHouseList.json";

        public ObsHusListeSingleton Singleton { get; set; }

        public DateTime CurrentDateTime { get; set; } = DateTime.Today;

        private DateTime _dateTime = DateTime.Today;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value.Date;
                OnPropertyChanged(nameof(DateTime)); }
        }

        private Hus _husTilListe;
        public Hus HusTilListe
        {
            get { return _husTilListe; }
            set { _husTilListe = value;
                OnPropertyChanged(nameof(HusTilListe)); }
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

        private Hus _selectedHusListView;
        public Hus SelectedHusListView
        {
            get { return _selectedHusListView; }
            set { _selectedHusListView = value;
                OnPropertyChanged(nameof(SelectedHusListView));
            }
        }

        private LåsListeSingleton _låsSingleton;
        public LåsListeSingleton LåsSingleton
        {
            get { return _låsSingleton; }
            set { _låsSingleton = value; }
        }

        

        public MainViewModel()
        {
            Singleton = ObsHusListeSingleton.Instance;
            HusListe = Singleton.HusListe;

            LåsSingleton = LåsListeSingleton.Instance;

            AddEvent = new RelayCommand(AddEventOnDateTime, null);
            DisplayEvent = new RelayCommand(DisplayEventOnDateTime, null);
            RemoveEvent = new RelayCommand(RemoveEventOnDateTime, null);
            RemoveHouse = new RelayCommand(RemoveHouseFromList, null);

            HusTilListe = new Hus();
            TilmeldsListe = new ObservableCollection<Hus>();
            Result = new ObservableCollection<Hus>();
            LoadJson();

        }

        public void RemoveEventOnDateTime()
        {
                
            if(SelectedHusListView != null) {
                TilmeldsListe.Remove(SelectedHusListView);
                DisplayEventOnDateTime();
                SaveList_Async(TilmeldsListe, FileNameForTilmeldsListe);
            } else { 
                MessageDialog noEvent = new MessageDialog("Vælg en husstand på listen!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }
        }

        public void AddEventOnDateTime()
        {
            if (LåsSingleton.LockedDatesList.Any(x => x.LåsDato <= CurrentDateTime) && LåsSingleton.LockedDatesList.Any(xy => xy.DateTimeID == DateTime))
            {
                MessageDialog dateLocked = new MessageDialog("Denne Dato er Låst!");
                dateLocked.Commands.Add(new UICommand { Label = "Ok" });
                dateLocked.ShowAsync().AsTask();
            } else {

            if (TilmeldsListe.Where(hus => hus.HusNr == HusListe[Singleton.SelectedIndex].HusNr).Any(hus => hus.DT.Any(husDt => husDt == DateTime)) == false)
            {
                HusListe[Singleton.SelectedIndex].DT.Add(DateTime);
                TilmeldsListe.Add(HusListe[Singleton.SelectedIndex]);
                DisplayEventOnDateTime();
                SaveList_Async(TilmeldsListe, FileNameForTilmeldsListe);
            } else
            {
                MessageDialog noEvent = new MessageDialog("Denne husstand er allerede tilmeldt!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }

           }
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
            }
        }

        public void RemoveHouseFromList()
        {
            if(HusListe.Count != 0) {
                Singleton.RemoveHouse(HusListe[Singleton.SelectedIndex]);
                SaveList_Async(HusListe, FileNameForHusListe);
                if(HusListe.Count > 0)
                {
                    Singleton.SelectedIndex = 0;
                }
            }
        }

        //Json til at gemme listen
        private async void SaveList_Async(Object objForSave, String FileName)
        {
     
            StorageFile LocalFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            String jsonSaveData = JsonConvert.SerializeObject(objForSave);


            await FileIO.WriteTextAsync(LocalFile, jsonSaveData);
        }

        //Json til at hente listen
        private async void LoadJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameForTilmeldsListe);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                TilmeldsListe = JsonConvert.DeserializeObject<ObservableCollection<Hus>>(jsonSaveData);
                DisplayEventOnDateTime();
            }
            catch (Exception)
            {
            }
        }

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

            if(propertyname == nameof(DateTime))
            {
                DisplayEventOnDateTime();
            }

        }


    }
}