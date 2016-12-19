using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FællesSpisning.Model;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Popups;

namespace FællesSpisning.ViewModel
{
    class NewHouseViewModel : INotifyPropertyChanged
    {

        public List<int> VoksneCBoxOptions { get; set; }
        public List<int> UngeCBoxOptions { get; set; }
        public List<int> Børn36CBoxOptions { get; set; }
        public List<int> BørnU3CBoxOptions { get; set; }

        //RelayCommands
        public RelayCommand AddNewHouseCommand { get; set; }

        //Konstant til at save HusListe
        const String FileName = "saveHouseList.json";

        //New house object for adding to list
        private Hus _nytHus;
        public Hus NytHus
        {
            get { return _nytHus; }
            set { _nytHus = value;
                OnPropertyChanged(nameof(NytHus));
            }
        }

        //HusNr Container
        private String _tempHusNr;
        public String TempHusNr
        {
            get { return _tempHusNr; }
            set { _tempHusNr = value; }
        }

        private String _outPutToUser;
        public String OutPutToUser
        {
            get { return _outPutToUser; }
            set { _outPutToUser = value;
                OnPropertyChanged(nameof(OutPutToUser));
            }
        }


        //HusListeSingleton
        public ObsHusListeSingleton HusListe { get; set; }
 

        public NewHouseViewModel()
        {
            AddNewHouseCommand = new RelayCommand(AddNewHouse, null);
            NytHus = new Model.Hus();
            AddCBoxOptions();
            this.HusListe = ObsHusListeSingleton.Instance;
        }

        public void AddNewHouse()
        {
            if((!String.IsNullOrEmpty(TempHusNr)) && (TempHusNr.All(char.IsDigit)))
            {

                Hus tempHusObj = new Hus();
                tempHusObj.AntalVoksne = VoksneCBoxOptions[NytHus.AntalVoksne];
                tempHusObj.AntalUnge = VoksneCBoxOptions[NytHus.AntalUnge];
                tempHusObj.AntalBørn = VoksneCBoxOptions[NytHus.AntalBørn];
                tempHusObj.AntalBørnU3 = VoksneCBoxOptions[NytHus.AntalBørnU3];
                tempHusObj.HusNr = int.Parse(TempHusNr);

                HusListe.AddNewHouse(tempHusObj);
                SaveList_Async();
                OutPutToUser = "Husstand blev oprettet!";
            } else
            {
                OutPutToUser = "";
                MessageDialog noEvent = new MessageDialog("Hus nummer skal være et tal!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }


        }

        //Add options to ComboBoxes
        public void AddCBoxOptions()
        {
            VoksneCBoxOptions = new List<int>() { 0, 1, 2, 3, 4, 5 };
            UngeCBoxOptions = new List<int>() { 0, 1, 2, 3, 4, 5 };
            Børn36CBoxOptions = new List<int>() { 0, 1, 2, 3, 4, 5 };
            BørnU3CBoxOptions = new List<int>() { 0, 1, 2, 3, 4, 5 };
        }

        private async void SaveList_Async()
        {

                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteTextAsync(LocalFile, HusListe.SaveJsonData());  
        }

        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
