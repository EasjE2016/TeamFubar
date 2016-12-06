using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FællesSpisning.Model;
using Windows.UI.Popups;
using FællesSpisning.ViewModel;

namespace FællesSpisning.ViewModel
{
    class AdminViewModel : INotifyPropertyChanged
    {

        public JobPerson NewJobPerson { get; set; }
        public List<string> CBoxJobType { get; set; }
        public RelayCommand AddPlanlægCommand { get; set; }

        private PlanListe _planliste;

        public PlanListe Planliste
        {
            get { return _planliste; }
            set { _planliste = value; OnPropertyChanged(nameof(Planliste)); }
        }

        private string jobpersonNavn;

        public string JobPersonNavn
        {
            get { return jobpersonNavn; }
            set { jobpersonNavn = value; OnPropertyChanged(nameof(JobPersonNavn)); }
        }


        public AdminViewModel()
        {
            Planliste = new PlanListe();
            NewJobPerson = new JobPerson();
            AddPlanlægCommand = new RelayCommand(AddNewPlanlæg, AddPlanlægCanExecute);
            AddCBoxType();
        }

        private bool AddPlanlægCanExecute()
        {
            return this.jobpersonNavn != string.Empty;
        }
        public void AddNewPlanlæg()
        {
            JobPerson addJobPerson = new JobPerson()
            {
                JobPersonNavn = this.JobPersonNavn,
            };
            Planliste.Add(addJobPerson);
            this.JobPersonNavn = string.Empty;
        }

        public void AddCBoxType()
        {
            CBoxJobType = new List<string>() { "Chefkok", "Kok", "Oprydder" };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
