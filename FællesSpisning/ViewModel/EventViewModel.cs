using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FællesSpisning.Model;
using System.Linq.Expressions;


namespace FællesSpisning.ViewModel
{
    class EventViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddEvent { get; set; }
        public RelayCommand DisplayEvent { get; set; }

        private DateTime _dateTime = DateTime.Today;
        public DateTime DateTime 
        {
            get { return _dateTime; }
            set { _dateTime = value.Date; OnPropertyChanged(nameof(DateTime)); }
        }

      
        private TestValues _test;
        public TestValues Test
        {
            get { return _test; }
            set { _test = value; OnPropertyChanged(nameof(Test)); }
        }

        private TestList _eventList;
        public TestList EventList
        {
            get { return _eventList; }
            set { _eventList = value; }
        }

        private TestValues _result;

        public TestValues Result
        {
            get { return _result; }
            set { _result = value; }
        }





        public EventViewModel()
        {
            AddEvent = new RelayCommand(AddEventOnDateTime, null);
            DisplayEvent = new RelayCommand(DisplayEventOnDateTime, null);

            Test = new TestValues();
            EventList = new TestList();
            Result = new TestValues();
                 
        }

        
        public void AddEventOnDateTime()
        {
            Test.EventTime = DateTime;             
            EventList.Add(Test);
        }

        public void DisplayEventOnDateTime()
        {
            _result = EventList.Single(x => x.EventTime == DateTime);

            //TestValues result = EventList.Contains(result.EventTime = DateTime);
            
            //List<int> list = new List<int>();
            //var result = list.Find
        }

        

        // propertychanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
