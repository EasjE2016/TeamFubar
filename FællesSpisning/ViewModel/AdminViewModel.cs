using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FællesSpisning.Model;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using Windows.Storage;
using Newtonsoft.Json;

namespace FællesSpisning.ViewModel
{
    class AdminViewModel : INotifyPropertyChanged
    {

        //Konstanter til filnavne
        const String FileNameJob = "saveJobListe.json";
        const String FileNameMenu = "saveMenuListe.json";

        //ComboBox options
        public List<string> JobPersonCBoxOptions { get; set; }
        public List<DateTime> ListeMedDateTimes { get; set; }

        public JobPerson Job { get; set; }
        public Menu Menu { get; set; }
        public int SelectedIndex { get; set; }

        public RelayCommand AddJobPersonCommand { get; set; }
        public RelayCommand RemoveJobPersonCommand { get; set; }
        public RelayCommand AddMenuCommand { get; set; }
        public RelayCommand RemoveMenuCommand { get; set; }
        public RelayCommand NyLåsCommand { get; set; }

        private DateTime _planDateTime = DateTime.Today;
        public DateTime PlanDateTime
        {
            get { return _planDateTime; }
            set
            {
                _planDateTime = value.Date;
                OnPropertyChanged(nameof(PlanDateTime));
            }
        }

        private PlanlægningSingleton _planSingleton;
        public PlanlægningSingleton PlanSingleton
        {
            get { return _planSingleton; }
            set { _planSingleton = value;
                OnPropertyChanged(nameof(PlanSingleton));
            }
        }

        public ObservableCollection<JobPerson> PlanListe { get; set; }
        public ObservableCollection<Menu> MenuListe { get; set; }

        private JobPerson _selectedJob;
        public JobPerson SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        private Menu _selectedMenu;
        public Menu SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                _selectedMenu = value;
                OnPropertyChanged(nameof(SelectedMenu));
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

        private ObservableCollection<LåstDates> _resultLock;
        public ObservableCollection<LåstDates> ResultLock
        {
            get { return _resultLock; }
            set { _resultLock = value;
                OnPropertyChanged(nameof(ResultLock));
            }
        }


        private ObservableCollection<LåstDates> _lockedDatesList;

        public ObservableCollection<LåstDates> LockedDatesList
        {
            get { return _lockedDatesList; }
            set { _lockedDatesList = value;
            }
        }


        public AdminViewModel()
        {
            PlanSingleton = PlanlægningSingleton.Instance;

            this.PlanListe = PlanSingleton.JobListe;
            this.MenuListe = PlanSingleton.MenuListe;
            this.LockedDatesList = LåsListeSingleton.Instance.LockedDatesList;

            ResultJob = new ObservableCollection<JobPerson>();
            ResultMenu = new ObservableCollection<Menu>();
            ResultLock = new ObservableCollection<LåstDates>();

            AddJobPersonCommand = new RelayCommand(AddNewJobPerson, null);
            RemoveJobPersonCommand = new RelayCommand(RemoveSelectedJobPerson, null);
            AddMenuCommand = new RelayCommand(AddNewMenu, null);
            RemoveMenuCommand = new RelayCommand(RemoveSelectedMenu, null);
            NyLåsCommand = new RelayCommand(AddNyLås, null);

            SelectedJob = new JobPerson();
            SelectedMenu = new Menu();

            Job = new JobPerson();
            Menu = new Menu();

            AddCBoxOptions();
            LoadJobJson();
            LoadMenuJson();

        }

        public void AddNyLås()
        {
            LåstDates tempLås = new LåstDates();
            tempLås.LåsDato = PlanDateTime;
            tempLås.DateTimeID = PlanDateTime;

            LåsListeSingleton.Instance.AddNewLock(tempLås);
            DisplayEventOnDateTime();

        }

        public void AddNewJobPerson()
        {

            JobPerson tempJob = new JobPerson();

            tempJob.JobDateTime = PlanDateTime;
            tempJob.JobPersonNavn = Job.JobPersonNavn;
            tempJob.JobPersonOpgave = JobPersonCBoxOptions[SelectedIndex];

            PlanSingleton.AddJobPerson(tempJob);
            PlanListe.Add(tempJob);
            SaveJobList_Async();
            DisplayEventOnDateTime();
        }


        public void AddNewMenu()
        {

            Menu tempMenu = new Menu();

            tempMenu.MenuDateTime = PlanDateTime;
            tempMenu.MenuMeal = Menu.MenuMeal;

            PlanSingleton.AddMenu(tempMenu);
            MenuListe.Add(tempMenu);
            SaveMenuList_Async();
            DisplayEventOnDateTime();

        }

        public void RemoveSelectedJobPerson()
        {
            if (SelectedJob != null)
            {
                PlanSingleton.RemoveJobPerson(SelectedJob);
                PlanListe.Remove(SelectedJob);
                SaveJobList_Async();
                DisplayEventOnDateTime();
            }
            else
            {
                MessageDialog noEvent = new MessageDialog("Vælg person for at slette!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }

        }


        public void RemoveSelectedMenu()
        {
            if (SelectedMenu != null)
            {
                PlanSingleton.RemoveMenu(SelectedMenu);
                MenuListe.Remove(SelectedMenu);
                SaveMenuList_Async();
                DisplayEventOnDateTime();
            }
            else
            {
                MessageDialog noEvent = new MessageDialog("Vælg menu for at slette!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }

        }

        public void DisplayEventOnDateTime()
        {
            ResultJob.Clear();
            ResultMenu.Clear();
            ResultLock.Clear();

            try
            {

                foreach (JobPerson personObj in PlanListe)
                {
                    if (personObj.JobDateTime == PlanDateTime)
                    {
                        ResultJob.Add(personObj);
                    }
                }

                foreach (Menu menuObj in MenuListe)
                {
                    if (menuObj.MenuDateTime == PlanDateTime)
                    {
                        ResultMenu.Add(menuObj);
                    }
                }

                foreach (LåstDates item in LockedDatesList)
                {
                    ResultLock.Add(item);
                }

            }
            catch (Exception)
            {
            }
        }

        //Add muligheder til ComboBox
        public void AddCBoxOptions()
        {
            JobPersonCBoxOptions = new List<string>() { "Chefkok", "Kok", "Oprydder" };
        }
        

        //Json Save
        public async void SaveJobList_Async()
        {
            StorageFile LocalFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileNameJob, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(LocalFile, PlanSingleton.SaveJsonDataJob());

        }

        public async void SaveMenuList_Async()
        {
            StorageFile LocalFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileNameMenu, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(LocalFile, PlanSingleton.SaveJsonDataMenu());
        }

        //Load Json
        private async void LoadJobJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameJob);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                PlanListe = JsonConvert.DeserializeObject<ObservableCollection<JobPerson>>(jsonSaveData);
                DisplayEventOnDateTime();
            }
            catch (Exception)
            {
            }
        }

        private async void LoadMenuJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(FileNameMenu);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                MenuListe = JsonConvert.DeserializeObject<ObservableCollection<Menu>>(jsonSaveData);
                DisplayEventOnDateTime();
            }
            catch (Exception)
            {
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

            if(propertyname == nameof(PlanDateTime))
            {
                DisplayEventOnDateTime();
            }
        }

    }
}
