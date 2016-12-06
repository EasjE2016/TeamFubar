using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FællesSpisning;
using System.ComponentModel;
using FællesSpisning.Model;
using Newtonsoft.Json;
using Windows.Storage;
using System.Collections.ObjectModel;

namespace FællesSpisning.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObsHusListeSingleton _instance;
        public ObsHusListeSingleton Instance
        {
            get { return _instance; }
            set { _instance = value;
                OnPropertyChanged(nameof(Instance));
            }
        }

        public ObservableCollection<Hus> HusListe { get; set; }

        public MainViewModel()
        {
            this.Instance = ObsHusListeSingleton._Instance;
            HusListe = Instance.HusListe;
        }

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }


    }
}
