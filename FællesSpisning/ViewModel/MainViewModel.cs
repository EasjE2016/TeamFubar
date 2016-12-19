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

        private Hus _selectedHusListView;
        public Hus SelectedHusListView
        {
            get { return _selectedHusListView; }
            set { _selectedHusListView = value;
                OnPropertyChanged(nameof(SelectedHusListView));
            }
        }

        private String _outPutToUser;
        public String OutPutToUser
        {
            get { return _outPutToUser; }
            set
            {
                _outPutToUser = value;
                OnPropertyChanged(nameof(OutPutToUser));
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

        }


        public void RemoveEventOnDateTime()
        {             
            if(SelectedHusListView != null) {
                PlanSingleton.RemoveFraTilmeld(SelectedHusListView);
                SaveList_Async(PlanSingleton.TilmeldsListe, FileNameTilmeldsListe);
                OutPutToUser = $"Husstand blev afmeldt fra d. {PlanSingleton.SingletonDateTime.ToString("MM/dd")}!";
            } else { 
                MessageDialog noEvent = new MessageDialog("Vælg en husstand på listen!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
                OutPutToUser = "";
            }
        }

        public void AddEventOnDateTime()
        {
            if (HusListe.Count > 0) {
            Hus tempHus = new Hus();
            tempHus.AntalBørnU3 = HusListe[HusListeSingleton.SelectedIndex].AntalBørnU3;
            tempHus.AntalBørn = HusListe[HusListeSingleton.SelectedIndex].AntalBørn;
            tempHus.AntalUnge = HusListe[HusListeSingleton.SelectedIndex].AntalUnge;
            tempHus.AntalVoksne = HusListe[HusListeSingleton.SelectedIndex].AntalVoksne;
            tempHus.HusNr = HusListe[HusListeSingleton.SelectedIndex].HusNr;
            tempHus.DT = PlanSingleton.SingletonDateTime;
            
            if (PlanSingleton.TilmeldsListe.Any(husObj => husObj.DT == tempHus.DT && husObj.HusNr == tempHus.HusNr) == false)
            {
                PlanSingleton.AddTilTilmeld(tempHus);
                SaveList_Async(PlanSingleton.TilmeldsListe, FileNameTilmeldsListe);
                OutPutToUser = $"Husstand blev tilmeldt til d. {PlanSingleton.SingletonDateTime.ToString("MM/dd")}!";
            }
                else
            {
                MessageDialog noEvent = new MessageDialog("Denne husstand er allerede tilmeldt!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
                OutPutToUser = "";
            }
            }
            else
            {
                MessageDialog noEvent = new MessageDialog("Ingen husstand er valgt!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
                OutPutToUser = "";
            }

        }

        public void RemoveHouseFromList()
        {
            if(HusListe.Count != 0) {
                OutPutToUser = $"Husstand {HusListe[HusListeSingleton.SelectedIndex].HusNr} blev slettet!";
                HusListeSingleton.RemoveHouse(HusListe[HusListeSingleton.SelectedIndex]);
                SaveList_Async(HusListe, FileNameForHusListe);
                if(HusListe.Count > 0)
                {
                    HusListeSingleton.SelectedIndex = 0;
                }
            }
            else
            {
                MessageDialog noEvent = new MessageDialog("Der er ingen husstand at slette!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
                OutPutToUser = "";
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