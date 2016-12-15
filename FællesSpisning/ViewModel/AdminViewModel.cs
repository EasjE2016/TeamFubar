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
        public RelayCommand RemoveLåsCommand { get; set; }


        private PlanlægningSingleton _planSingleton;
        public PlanlægningSingleton PlanSingleton
        {
            get { return _planSingleton; }
            set { _planSingleton = value;
                OnPropertyChanged(nameof(PlanSingleton));
            }
        }

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


        public AdminViewModel()
        {

            PlanSingleton = PlanlægningSingleton.Instance;

            AddJobPersonCommand = new RelayCommand(AddNewJobPerson, null);
            RemoveJobPersonCommand = new RelayCommand(RemoveSelectedJobPerson, null);
            AddMenuCommand = new RelayCommand(AddNewMenu, null);
            RemoveMenuCommand = new RelayCommand(RemoveSelectedMenu, null);
            NyLåsCommand = new RelayCommand(AddNyLås, null);
            RemoveLåsCommand = new RelayCommand(RemoveLås, null);

            SelectedJob = new JobPerson();
            SelectedMenu = new Menu();

            Job = new JobPerson();
            Menu = new Menu();

            AddCBoxOptions();

        }

        public void AddNyLås()
        {
            if(PlanSingleton.LockedDatesDic.ContainsKey(PlanSingleton.SingletonDateTime))
            {
                MessageDialog locked = new MessageDialog("Dato er allerede låst");
                locked.Commands.Add(new UICommand { Label = "Ok" } );
                locked.ShowAsync().AsTask();
            } else
            {
                bool BoolLock = true;
                PlanSingleton.AddNewLock(PlanSingleton.SingletonDateTime, BoolLock);
                PlanSingleton.SaveJsonLockDates();
            }
        }

        public void RemoveLås()
        {
            if(PlanSingleton.LockedDatesDic.Count > 0)
            {
                foreach (DateTime lockObj in PlanSingleton.LockedDatesDic.Keys.ToList())
                {
                    if(lockObj == PlanSingleton.SingletonDateTime)
                    {
                        PlanSingleton.RemoveLock();
                        PlanSingleton.SaveJsonLockDates();
                    }
                }
            }
        }

        public void AddNewJobPerson()
        {

            JobPerson tempJob = new JobPerson();

            tempJob.JobDateTime = PlanSingleton.SingletonDateTime;
            tempJob.JobPersonNavn = Job.JobPersonNavn;
            tempJob.JobPersonOpgave = JobPersonCBoxOptions[SelectedIndex];

            PlanSingleton.AddJobPerson(tempJob);
            SaveJobList_Async();
        }


        public void AddNewMenu()
        {

            Menu tempMenu = new Menu();

            tempMenu.MenuDateTime = PlanSingleton.SingletonDateTime;
            tempMenu.MenuMeal = Menu.MenuMeal;

            PlanSingleton.AddMenu(tempMenu);
            SaveMenuList_Async();

        }

        public void RemoveSelectedJobPerson()
        {
            if (SelectedJob != null)
            {
                PlanSingleton.RemoveJobPerson(SelectedJob);
                SaveJobList_Async();
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
                SaveMenuList_Async();
            }
            else
            {
                MessageDialog noEvent = new MessageDialog("Vælg menu for at slette!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
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

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            
        }

    }
}
