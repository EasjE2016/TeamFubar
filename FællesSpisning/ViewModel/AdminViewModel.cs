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
        public List<string> JobPersonCBoxOptions { get; set; }

        public JobPerson PlanToListe { get; set; }
        public int SelectedIndex { get; set; }
        
        public RelayCommand AddJobPersonCommand { get; set; }
        public RelayCommand RemoveJobPersonCommand { get; set; }
        public RelayCommand DisplayPlanDate { get; set; }
        public RelayCommand NyLåsCommand { get; set; }
        public RelayCommand RemoveLåsCommand { get; set; }
        public RelayCommand DisplayLåsDate { get; set; }

        const string PlanFileSave = "savePlanListe.json";
        //Lock Test

            // Alt json skal flyttes over i lås.cs

        //const String LåsStartSave = "saveStartLås.json";
        //const String LåsEndSave = "saveEndLås.json";

        //Lock Test

        private DateTime _planDateTime = DateTime.Today;
        public DateTime PlanDateTime
        {
            get { return _planDateTime; }
            set { _planDateTime = value.Date;
                OnPropertyChanged(nameof(PlanDateTime)); 
            }
        }

        
        private PlanListe _listeOfPlans;
        public PlanListe ListeOfPlans
        {
            get { return _listeOfPlans; }
            set { _listeOfPlans = value; OnPropertyChanged(nameof(ListeOfPlans)); }
        }

        private JobPerson _selectedJobPerson;
        public JobPerson SelectedJobPerson
        {
            get { return _selectedJobPerson; }
            set { _selectedJobPerson = value; OnPropertyChanged(nameof(SelectedJobPerson)); }
        }

        private LåsProperties _selectedLås;
        public LåsProperties SelectedLås
        {
            get { return _selectedLås; }
            set { _selectedLås = value; OnPropertyChanged(nameof(SelectedLås)); }
        }


        private ObservableCollection<JobPerson> _result;
        public  ObservableCollection<JobPerson> Result
        {
            get { return _result; }
            set { _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        private DateTime _låsDateID = DateTime.Today;
        public DateTime LåsDateID
        {
            get { return _låsDateID; }
            set { _låsDateID = value; }
        }
        private ObservableCollection<LåsProperties> _listOfLocks;
        public ObservableCollection<LåsProperties> ListOfLocks
        {
            get { return _listOfLocks; }
            set { _listOfLocks = value; OnPropertyChanged(nameof(ListOfLocks)); }
        }

        private ObservableCollection<LåsProperties> _displayLås;
        public ObservableCollection<LåsProperties> DisplayLås
        {
            get { return _displayLås; }
            set { _displayLås = value; OnPropertyChanged(nameof(DisplayLås)); }
        }

        public AdminViewModel()
        {
            ListeOfPlans = new PlanListe();
            Result = new ObservableCollection<JobPerson>();
            DisplayLås = new ObservableCollection<LåsProperties>();
            
            SelectedJobPerson = new JobPerson();
            PlanToListe = new JobPerson();
            SelectedLås = new LåsProperties();

            AddJobPersonCommand = new RelayCommand(AddNewJobPerson, null);
            RemoveJobPersonCommand = new RelayCommand(RemoveSelectedJobPerson, null);
            NyLåsCommand = new RelayCommand(AddNyLås, null);
            RemoveLåsCommand = new RelayCommand(RemoveSelectedLås, null);

            ListOfLocks = Lås.LåsFunktioner.ListOfLockedDates;
            
            AddCBoxOptions();
            LoadJson();

        }
        

        public void AddNyLås()
        {
            LåsProperties tempContainer = new LåsProperties();
            tempContainer.DateTimeID = PlanDateTime;
            tempContainer.LåsDato = LåsDateID;

            ListOfLocks.Add(tempContainer);
            DisplayLockOnDateTime();
        }


        public void AddNewJobPerson()
        {

                JobPerson tempListe = new JobPerson();

                tempListe.JobDateTime = PlanDateTime;
                tempListe.JobPersonNavn = PlanToListe.JobPersonNavn;
                tempListe.JobPersonOpgave = JobPersonCBoxOptions[SelectedIndex];
                tempListe.Menu = JobPersonCBoxOptions[SelectedIndex];

                ListeOfPlans.Add(tempListe);

                SaveList_Async(ListeOfPlans, PlanFileSave);
                DisplayEventOnDateTime();

        }

        public void RemoveSelectedLås()
        {
            if (SelectedLås != null)
            {
                ListOfLocks.Remove(SelectedLås);
                DisplayLockOnDateTime();
            }
            else
            {
                MessageDialog noLås = new MessageDialog("Vælg lås på liste!");
                noLås.Commands.Add(new UICommand { Label = "Ok" } );
                noLås.ShowAsync().AsTask();
            }
        }

        public void RemoveSelectedJobPerson()
        {
            if (SelectedJobPerson != null)
            {
                ListeOfPlans.Remove(SelectedJobPerson);
                DisplayEventOnDateTime();
                SaveList_Async(ListeOfPlans, PlanFileSave);

            }
            else
            {
                MessageDialog noEvent = new MessageDialog("Vælg en husstand på listen!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }

        }

        public void DisplayEventOnDateTime()
        {
            Result.Clear();

            try
            {

                foreach (JobPerson personObj in ListeOfPlans)
                {
                    if (personObj.JobDateTime == PlanDateTime)
                    {
                        Result.Add(personObj);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DisplayLockOnDateTime()
        {
            DisplayLås.Clear();

            try
            {
                foreach (LåsProperties x in ListOfLocks)
                {
                    if (x.DateTimeID == PlanDateTime)
                    {
                        DisplayLås.Add(x);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void AddCBoxOptions()
        {
            JobPersonCBoxOptions = new List<string>() { "Chefkok", "Kok", "Oprydder", "Menu" };
        }
        
        private async void SaveList_Async(Object objForSave, String FileName)
        {

            StorageFile LocalFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            String jsonSaveData = JsonConvert.SerializeObject(objForSave);


            await FileIO.WriteTextAsync(LocalFile, jsonSaveData);
        }

        private async void LoadJson()
        {
            try
            {
                StorageFile LocalFile = await ApplicationData.Current.LocalFolder.GetFileAsync(PlanFileSave);
                String jsonSaveData = await FileIO.ReadTextAsync(LocalFile);
                ListeOfPlans = JsonConvert.DeserializeObject<PlanListe>(jsonSaveData);
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

            if (propertyname == nameof(PlanDateTime))
            {
                DisplayEventOnDateTime();
                DisplayLockOnDateTime();

            }

        }

    }
}
