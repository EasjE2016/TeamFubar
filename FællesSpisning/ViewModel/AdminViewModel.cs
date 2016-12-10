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

        const string PlanFileSave = "savePlanListe.json";

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

        private ObservableCollection<JobPerson> _result;
        public  ObservableCollection<JobPerson> Result
        {
            get { return _result; }
            set { _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        


        public AdminViewModel()
        {
            ListeOfPlans = new PlanListe();
            Result = new ObservableCollection<JobPerson>();

            SelectedJobPerson = new JobPerson();
            PlanToListe = new JobPerson();

            DisplayPlanDate = new RelayCommand(DisplayEventOnDateTime, null);
            AddJobPersonCommand = new RelayCommand(AddNewJobPerson, null);
            RemoveJobPersonCommand = new RelayCommand(RemoveSelectedJobPerson, null);

            AddCBoxOptions();
            LoadJson();


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


        public void AddCBoxOptions()
        {
            JobPersonCBoxOptions = new List<string>() { "Chefkok", "Kok", "Oprydder", "Menu" };
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
            }
        }

    }
}
