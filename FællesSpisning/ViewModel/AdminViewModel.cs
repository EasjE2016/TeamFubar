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
        public RelayCommand AddJobPersonCommand { get; set; }
        public RelayCommand RemoveJobPersonCommand { get; set; }

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

        public AdminViewModel()
        {
            Planliste = new PlanListe();
            _selectedJobPerson = new JobPerson();
            NewJobPerson = new JobPerson();
            AddJobPersonCommand = new RelayCommand(AddNewJobPerson, null);
            RemoveJobPersonCommand = new RelayCommand(RemoveSelectedJobPerson, null);
        }

        public void AddNewJobPerson()
        {
            Planliste.Add(NewJobPerson);
        }

        public void RemoveSelectedJobPerson()
        {
            Planliste.Remove(SelectedJobPerson);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
