using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Popups;
using System.ComponentModel;
using System.Diagnostics;

namespace FællesSpisning.Model
{
    class ObsHusListeSingleton : INotifyPropertyChanged

    {
        //Konstant Filnavn
        const String FileName = "saveHouseList.json";

        private static ObsHusListeSingleton _instance;
        public static ObsHusListeSingleton Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new ObsHusListeSingleton();
                }
                return _instance;
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


        public ObservableCollection<Hus> HusListe { get; set; }

        private ObsHusListeSingleton()
        {
            HusListe = new ObservableCollection<Hus>();
            LoadJson();
        }

        public void AddNewHouse(Hus NytHus)
        {
            HusListe.Add(NytHus);
            SelectedIndex = 0;
        }

        public void RemoveHouse(Hus SelectedHus)
        {
            HusListe.Remove(SelectedHus);
        }

        private async void LoadJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                HusListe = JsonConvert.DeserializeObject<ObservableCollection<Hus>>(jsonSaveData);
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
        }

        public String SaveJsonData()
        {
            String jsonSaveData = JsonConvert.SerializeObject(HusListe);

            return jsonSaveData;
        }

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        }

    }
}
