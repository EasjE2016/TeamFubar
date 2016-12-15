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
using System.Collections.Specialized;

namespace FællesSpisning.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        //Relay Commands
        public RelayCommand AddEvent { get; set; }
        public RelayCommand DisplayEvent { get; set; }
        public RelayCommand RemoveEvent { get; set; }
        public RelayCommand RemoveHouse { get; set; }

        //Konstant filnavn
        const String FileNameTilmeldsListe = "saveTilmeldsListe.json";
        const String FileNameForHusListe = "saveHouseList.json";

        public ObsHusListeSingleton HusListeSingleton { get; set; }
        public PlanlægningSingleton PlanSingleton { get; set; }

        public DateTime CurrentDateTime { get; set; } = DateTime.Today;

        private ObservableCollection<Hus> _husListe;
        public ObservableCollection<Hus> HusListe
        {
            get { return _husListe; }
            set
            {
                _husListe = value;
                OnPropertyChanged(nameof(HusListe));
            }
        }

        private Hus _husTilListe;
        public Hus HusTilListe
        {
            get { return _husTilListe; }
            set { _husTilListe = value;
                OnPropertyChanged(nameof(HusTilListe)); }
        }

        private Hus _selectedHusListView;
        public Hus SelectedHusListView
        {
            get { return _selectedHusListView; }
            set { _selectedHusListView = value;
                OnPropertyChanged(nameof(SelectedHusListView));
            }
        }
        

        public MainViewModel()
        {
            HusListeSingleton = ObsHusListeSingleton.Instance;
            HusListe = HusListeSingleton.HusListe;
            
            PlanSingleton = PlanlægningSingleton.Instance;

            AddEvent = new RelayCommand(AddEventOnDateTime, null);
            RemoveEvent = new RelayCommand(RemoveEventOnDateTime, null);
            RemoveHouse = new RelayCommand(RemoveHouseFromList, null);

            HusTilListe = new Hus();

        }


        public void RemoveEventOnDateTime()
        {             
            if(SelectedHusListView != null) {
                PlanSingleton.TilmeldsListe.Remove(SelectedHusListView);
                PlanSingleton.DisplayTilmeldsListeOnDateTime();
                SaveList_Async(PlanSingleton.TilmeldsListe, FileNameTilmeldsListe);
            } else { 
                MessageDialog noEvent = new MessageDialog("Vælg en husstand på listen!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }
        }

        public void AddEventOnDateTime()
        {
            if (PlanSingleton.TilmeldsListe.Where(hus => hus.HusNr == HusListe[HusListeSingleton.SelectedIndex].HusNr).Any(hus => hus.DT.Any(husDt => husDt == PlanSingleton.SingletonDateTime)) == false)
            {
                HusListe[HusListeSingleton.SelectedIndex].DT.Add(PlanSingleton.SingletonDateTime);
                PlanSingleton.TilmeldsListe.Add(HusListe[HusListeSingleton.SelectedIndex]);
                PlanSingleton.DisplayTilmeldsListeOnDateTime();
                SaveList_Async(PlanSingleton.TilmeldsListe, FileNameTilmeldsListe);
            } else
            {
                MessageDialog noEvent = new MessageDialog("Denne husstand er allerede tilmeldt!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }

        }

        public void RemoveHouseFromList()
        {
            if(HusListe.Count != 0) {
                HusListeSingleton.RemoveHouse(HusListe[HusListeSingleton.SelectedIndex]);
                SaveList_Async(HusListe, FileNameForHusListe);
                if(HusListe.Count > 0)
                {
                    HusListeSingleton.SelectedIndex = 0;
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


        //INotifyPropertyChanged implementeret
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        }


    }
}