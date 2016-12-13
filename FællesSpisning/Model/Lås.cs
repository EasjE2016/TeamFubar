using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FællesSpisning.Model
{
    class Lås
    {
        const String Filename = "saveLåsListe.json";
  
        private static Lås _låsFunktioner;
        public static Lås LåsFunktioner
        {
            get
            {
                if (_låsFunktioner == null)
                {
                    _låsFunktioner = new Lås();
                }
                return _låsFunktioner;
            }
        }
        
        public ObservableCollection<LåsProperties> ListOfLockedDates { get; set; }

        public Lås()
        {
            ListOfLockedDates = new ObservableCollection<LåsProperties>();
            LoadJson();

        }

        private async void LoadJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(Filename);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                ListOfLockedDates = JsonConvert.DeserializeObject<ObservableCollection<LåsProperties>>(jsonSaveData); 
                    
            }
            catch (Exception)
            {
            }
        }

        public String SaveJsonData()
        {
            String jsonSaveData = JsonConvert.SerializeObject(ListOfLockedDates);

            return jsonSaveData;
        }









    }

}
