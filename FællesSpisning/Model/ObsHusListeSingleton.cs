using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Popups;

namespace FællesSpisning.Model
{
    class ObsHusListeSingleton

    {
        //Konstant Filnavn
        const String FileName = "saveHouseList.json";

        private static ObsHusListeSingleton _instance;
        public static ObsHusListeSingleton _Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new ObsHusListeSingleton();
                }
                return _instance;
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
            catch (Exception)
            {
            }
        }

        public String SaveJsonData()
        {
            String jsonSaveData = JsonConvert.SerializeObject(HusListe);

            return jsonSaveData;
        }

    }
}
