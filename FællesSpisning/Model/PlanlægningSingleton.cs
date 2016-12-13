using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace FællesSpisning.Model
{
    class PlanlægningSingleton : INotifyPropertyChanged
    {

        private static PlanlægningSingleton _instace;
        public static PlanlægningSingleton Instance
        {
            get
            {
                if (_instace == null)
                {
                    _instace = new PlanlægningSingleton();
                }
                return _instace;
            }
        }

        //Konstanter til filnavne
        const String FileNameJob = "saveJobListe.json";
        const String FileNameMenu = "saveMenuListe.json";

        //public ObservableCollection<JobPerson> JobListe { get; set; }
        //public ObservableCollection<Menu> MenuListe { get; set; }

        private ObservableCollection<JobPerson> _jobListe;

        public ObservableCollection<JobPerson> JobListe
        {
            get { return _jobListe; }
            set { _jobListe = value;
                OnPropertyChanged(nameof(JobListe));
            }
        }

        private ObservableCollection<Menu> _menuListe;

        public ObservableCollection<Menu> MenuListe
        {
            get { return _menuListe; }
            set { _menuListe = value;
                OnPropertyChanged(nameof(MenuListe));
            }
        }

        private PlanlægningSingleton()
        {
            JobListe = new ObservableCollection<JobPerson>();
            MenuListe = new ObservableCollection<Menu>();
        }

        public void AddJobPerson(JobPerson NyPerson)
        {
            JobListe.Add(NyPerson);
        }

        public void RemoveJobPerson(JobPerson SelectedPerson)
        {
            JobListe.Remove(SelectedPerson);
        }

        public void AddMenu(Menu NyMenu)
        {
            MenuListe.Add(NyMenu);
        }

        public void RemoveMenu(Menu SelectedMenu)
        {
            MenuListe.Remove(SelectedMenu);
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

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        }

    }
}
