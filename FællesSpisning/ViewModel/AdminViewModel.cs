using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FællesSpisning.Model;
using Windows.UI.Popups;

namespace FællesSpisning.ViewModel
{
    class AdminViewModel : INotifyPropertyChanged
    {
        public List<string> JobPersonCBoxOptions { get; set; }
        public JobPerson NewJobPerson { get; set; }
        public RelayCommand AddJobPersonCommand { get; set; }
        public RelayCommand RemoveJobPersonCommand { get; set; }

        private DateTime _planDateTime = DateTime.Today;

        public DateTime PlanDateTime
        {
            get { return _planDateTime; }
            set { _planDateTime = value.Date; }
        }


        private PlanListe _planliste;
        public PlanListe Planliste
        {
            get { return _planliste; }
            set { _planliste = value; OnPropertyChanged(nameof(Planliste)); }
        }

        private JobPerson _selectedJobPerson;
        public JobPerson SelectedJobPerson
        {
            get { return _selectedJobPerson; }
            set { _selectedJobPerson = value; OnPropertyChanged(nameof(SelectedJobPerson)); }
        }

        public int SelectedIndex { get; set; }


        public AdminViewModel()
        {
            Planliste = new PlanListe();
            _selectedJobPerson = new JobPerson();
            NewJobPerson = new JobPerson();
            AddJobPersonCommand = new RelayCommand(AddNewJobPerson, null);
            RemoveJobPersonCommand = new RelayCommand(RemoveSelectedJobPerson, null);
            AddCBoxOptions();
        }

        public void AddCBoxOptions()
        {
            JobPersonCBoxOptions = new List<string>() { "Chefkok", "Kok", "Oprydder" };
        }

        public void AddNewJobPerson()
        {
            JobPerson tempListe = new JobPerson();

            tempListe.JobDateTime = PlanDateTime;
            tempListe.JobPersonNavn = NewJobPerson.JobPersonNavn;
            tempListe.JobPersonOpgave = JobPersonCBoxOptions[SelectedIndex];

            if (Planliste.Contains(new JobPerson { JobDateTime = PlanDateTime }))
            {
                MessageDialog eventAlreadyPresent = new MessageDialog("Allerede planlagt en begivenhed på denne dato");
                eventAlreadyPresent.Commands.Add(new UICommand { Label = "Ok" });
                eventAlreadyPresent.ShowAsync().AsTask();

            }
            else
            {
                Planliste.Add(tempListe);
            }
        }




        public void RemoveSelectedJobPerson()
        {
            try
            {
                Planliste.Remove(Planliste.Where(x => x.JobDateTime == PlanDateTime).Single());
            }
            catch (Exception)
            {
                MessageDialog noEvent = new MessageDialog("Ingen Begivenhed planlægt på dato");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
