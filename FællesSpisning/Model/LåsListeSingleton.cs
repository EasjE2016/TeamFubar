using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class LåsListeSingleton : INotifyPropertyChanged
    {

        private static LåsListeSingleton _instance;
        public static LåsListeSingleton Instance
        {
            get {
                if(_instance == null)
                {
                    _instance = new LåsListeSingleton();
                }
                return _instance; }
            
        }

        //public ObservableCollection<LåstDates> LockedDatesList { get; set; }

        private ObservableCollection<LåstDates> _lockedDatesList;
        public ObservableCollection<LåstDates> LockedDatesList
        {
            get { return _lockedDatesList; }
            set { _lockedDatesList = value;
                OnPropertyChanged(nameof(LockedDatesList));
            }
        }


        private LåsListeSingleton()
        {
            LockedDatesList = new ObservableCollection<LåstDates>();
        }

        public void AddNewLock(LåstDates NyDato)
        {
            LockedDatesList.Add(NyDato);
        }

        public void RemoveLock(LåstDates DatoForRemoval)
        {
            LockedDatesList.Remove(DatoForRemoval);
        }

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        }

    }
}
