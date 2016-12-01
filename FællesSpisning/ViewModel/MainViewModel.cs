using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FællesSpisning;
using System.ComponentModel;

namespace FællesSpisning.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {

        //INotifyPropertyChanged implementeret
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }


    }
}
