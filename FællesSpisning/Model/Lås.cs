using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class Lås : INotifyPropertyChanged
    {
        private DateTime _startDato;
        public DateTime StartDato
        {
            get { return _startDato; }
            set { _startDato = value.Date; OnPropertyChanged(nameof(StartDato)); }
        }

        private DateTime _endDato;
        public DateTime EndDato
        {
            get { return _endDato; }
            set { _endDato = value.Date; OnPropertyChanged(nameof(EndDato)); }
        }

        private static Lås _låsListeFunktioner;
        public static Lås LåsListeFunktioner
        {
            get
            {
                if (_låsListeFunktioner == null)
                {
                    _låsListeFunktioner = new Lås();
                }
                return _låsListeFunktioner;
            }
        }

        public void SetStartDato(DateTime startDato)
        {
            StartDato = startDato;
        }

        public void SetEndDato(DateTime endDato)
        {
            EndDato = endDato;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        }

}
