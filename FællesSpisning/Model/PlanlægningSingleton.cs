using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using System.Diagnostics;

namespace FællesSpisning.Model
{
    class PlanlægningSingleton : INotifyPropertyChanged
    {

        private static PlanlægningSingleton _instance;
        public static PlanlægningSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlanlægningSingleton();
                }
                return _instance;
            }
        }

        //Konstanter til filnavne
        const String FileNameJob = "saveJobListe.json";
        const String FileNameMenu = "saveMenuListe.json";
        const String FileNameTilmeldsListe = "saveTilmeldsListe.json";
        const String FileNameLock = "saveLåsDic.json";

        public ObservableCollection<JobPerson> JobListe { get; set; }
        public ObservableCollection<Menu> MenuListe { get; set; }
        public ObservableCollection<Hus> TilmeldsListe { get; set; }

        private DateTime _singletonDateTime = DateTime.Today;
        public DateTime SingletonDateTime
        {
            get { return _singletonDateTime; }
            set
            {
                _singletonDateTime = value.Date;
                OnPropertyChanged(nameof(SingletonDateTime));
            }
        }

        private ObservableCollection<Menu> _resultMenu;
        public ObservableCollection<Menu> ResultMenu
        {
            get { return _resultMenu; }
            set
            {
                _resultMenu = value;
                OnPropertyChanged(nameof(ResultMenu));
            }
        }

        private ObservableCollection<JobPerson> _resultJob;
        public ObservableCollection<JobPerson> ResultJob
        {
            get { return _resultJob; }
            set
            {
                _resultJob = value;
                OnPropertyChanged(nameof(ResultJob));
            }
        }

        private ObservableCollection<Hus> _resultTilmeldte;
        public ObservableCollection<Hus> ResultTilmeldte
        {
            get { return _resultTilmeldte; }
            set
            {
                _resultTilmeldte = value;
                OnPropertyChanged(nameof(ResultTilmeldte));
            }
        }

        private Dictionary<DateTime, bool> _lockedDatesDic;
        public Dictionary<DateTime, bool> LockedDatesDic
        {
            get { return _lockedDatesDic; }
            set { _lockedDatesDic = value;
                OnPropertyChanged(nameof(LockedDatesDic));
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }



        private PlanlægningSingleton()
        {
            JobListe = new ObservableCollection<JobPerson>();
            MenuListe = new ObservableCollection<Menu>();
            TilmeldsListe = new ObservableCollection<Hus>();
            ResultTilmeldte = new ObservableCollection<Hus>();
            ResultMenu = new ObservableCollection<Menu>();
            ResultJob = new ObservableCollection<JobPerson>();
            LockedDatesDic = new Dictionary<DateTime, bool>();
            IsEnabled = true;
            LoadTilmeldsListeJson();
            LoadMenuJson();
            LoadJobJson();
            LoadLockedDatesJson();

        }

        public void AddJobPerson(JobPerson NyPerson)
        {
            JobListe.Add(NyPerson);
            DisplayJobOnDateTime();
        }

        public void RemoveJobPerson(JobPerson SelectedPerson)
        {
            JobListe.Remove(SelectedPerson);
            DisplayJobOnDateTime();
        }

        public void AddMenu(Menu NyMenu)
        {
            MenuListe.Add(NyMenu);
            DisplayMenuOnDateTime();
        }

        public void RemoveMenu(Menu SelectedMenu)
        {
            MenuListe.Remove(SelectedMenu);
            DisplayMenuOnDateTime();
        }

        public void AddNewLock(DateTime NyDato, bool IsLocked)
        {
            LockedDatesDic.Add(NyDato, IsLocked);
            DisplayTilmeldsListeOnDateTime();
        }

        public void RemoveLock()
        {
            LockedDatesDic.Remove(SingletonDateTime);
            DisplayTilmeldsListeOnDateTime();
        }

        public void AddTilTilmeld(Hus NyHus)
        {
            TilmeldsListe.Add(NyHus);
            DisplayTilmeldsListeOnDateTime();
        }

        public void RemoveFraTilmeld(Hus SelectedHus)
        {
            TilmeldsListe.Remove(SelectedHus);
            DisplayTilmeldsListeOnDateTime();
        }

        public String SaveJsonLockDates()
        {
            String jsonSaveData = JsonConvert.SerializeObject(LockedDatesDic);
            return jsonSaveData;
        }

        public String SaveJsonDataJob()
        {
            String jsonSaveData = JsonConvert.SerializeObject(JobListe);
            return jsonSaveData;
        }

        public String SaveJsonDataMenu()
        {
            String jsonSaveData = JsonConvert.SerializeObject(MenuListe);
            return jsonSaveData;
        }

        //Load Json
        private async void LoadJobJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameJob);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                this.JobListe = JsonConvert.DeserializeObject<ObservableCollection<JobPerson>>(jsonSaveData);
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
            DisplayJobOnDateTime();
        }

        private async void LoadMenuJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameMenu);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                this.MenuListe = JsonConvert.DeserializeObject<ObservableCollection<Menu>>(jsonSaveData);
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
            DisplayMenuOnDateTime();
        }

        private async void LoadTilmeldsListeJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameTilmeldsListe);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                this.TilmeldsListe = JsonConvert.DeserializeObject<ObservableCollection<Hus>>(jsonSaveData);
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
            DisplayTilmeldsListeOnDateTime();
        }

        private async void LoadLockedDatesJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameLock);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                this.LockedDatesDic = JsonConvert.DeserializeObject<Dictionary<DateTime, bool>>(jsonSaveData);
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
            DisplayTilmeldsListeOnDateTime();
        }

        public void DisplayMenuOnDateTime()
        {
            try
            {
                ResultMenu.Clear();

                foreach (Menu menuObj in MenuListe)
                {
                    if (menuObj.MenuDateTime == SingletonDateTime)
                    {
                        ResultMenu.Add(menuObj);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
        }

        public void DisplayJobOnDateTime()
        {
            try {

                ResultJob.Clear();

                foreach (JobPerson jobObj in JobListe)
                {
                    if(jobObj.JobDateTime == SingletonDateTime)
                    {
                        ResultJob.Add(jobObj);
                    }
                  }
            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }
        }

        public void DisplayTilmeldsListeOnDateTime()
        {

            try
            {
                if (LockedDatesDic.Any(lockObj => lockObj.Key == SingletonDateTime && lockObj.Value == true))
                {
                    IsEnabled = false;
                }
                else
                {
                    IsEnabled = true;
                }


                ResultTilmeldte.Clear();

                foreach (Hus husObj in TilmeldsListe)
                {
                    if(husObj.DT == SingletonDateTime)
                    {
                        ResultTilmeldte.Add(husObj);
                    }
                }

            }
            catch (Exception e)
            {
                Debug.Write($"Exception: { e }");
            }

        }

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

            if(propertyname == nameof(SingletonDateTime))
            {
                DisplayJobOnDateTime();
                DisplayMenuOnDateTime();
                DisplayTilmeldsListeOnDateTime();
            }

        }

    }
}