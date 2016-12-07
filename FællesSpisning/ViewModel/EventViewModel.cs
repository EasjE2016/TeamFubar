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
    class EventViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddEvent { get; set; }
        public RelayCommand DisplayEvent { get; set; }
        public RelayCommand RemoveEvent { get; set; }

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
            set { _result = value; OnPropertyChanged(nameof(Result)); }
        }


        public EventViewModel()
        {
            AddEvent = new RelayCommand(AddEventOnDateTime, null);
            DisplayEvent = new RelayCommand(DisplayEventOnDateTime, null);
            RemoveEvent = new RelayCommand(RemoveEventOnDateTime, null);

            Test = new TestValues();
            EventList = new TestList();
            Result = new TestValues();

        }


        public void RemoveEventOnDateTime()
        {
            try
            {
                EventList.Remove(EventList.Where(x => x.EventTime == DateTime).Single());
            }
            catch (Exception)
            {

                MessageDialog noEvent = new MessageDialog("Ingen Begivenhed planlægt på dato");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();

            }
        }

        public void AddEventOnDateTime()    
        {
            TestValues tempEvent = new TestValues();
            tempEvent.EventTime = DateTime;
            tempEvent.EventName = Test.EventName;



            if (EventList.Contains(new TestValues { EventTime = DateTime } ))
            {
                MessageDialog eventAlreadyPresent = new MessageDialog("Allerede planlagt en begivenhed på denne dato");
                eventAlreadyPresent.Commands.Add(new UICommand { Label = "Ok" });
                eventAlreadyPresent.ShowAsync().AsTask();

            }
            else
            {
                EventList.Add(tempEvent);
            }

        }

        public void DisplayEventOnDateTime()
        {
            try
            {
                Result = EventList.Where(x => x.EventTime == DateTime).First();
            }
            catch (Exception)
            {

                MessageDialog noEvent = new MessageDialog("Ingen Begivenhed planlægt på dato");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();

            }
        }
        
        // propertychanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}
