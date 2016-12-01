using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FællesSpisning.ViewModel
{
    class NewHouseViewModel : INotifyPropertyChanged
    {

        public List<int> VoksneCBoxOptions { get; set; }
        public List<int> UngeCBoxOptions { get; set; }
        public List<int> Børn36CBoxOptions { get; set; }
        public List<int> BørnU3CBoxOptions { get; set; }

        public NewHouseViewModel()
        {
            AddCBoxOptions();
        }

        //Add options to ComboBoxes
        public void AddCBoxOptions()
        {
            VoksneCBoxOptions = new List<int>() { 1, 2, 3, 4, 5 };
            UngeCBoxOptions = new List<int>() { 1, 2, 3, 4, 5 };
            Børn36CBoxOptions = new List<int>() { 1, 2, 3, 4, 5 };
            BørnU3CBoxOptions = new List<int>() { 1, 2, 3, 4, 5 };
        }

        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
