using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class LåsListe : INotifyPropertyChanged
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

        private static LåsListe _låsListeFunktioner;
        public static LåsListe LåsListeFunktioner
        {
            get
            {
                if (_låsListeFunktioner == null)
                {
                    _låsListeFunktioner = new LåsListe();
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

            //public DateTime DateToCheck { get; set; }

            //public DateTime CheckDate() Brug som if statement.
            //{
            //    return DateToCheck >= LåsDatoStart && DateToCheck < LåsDatoEnd;
            //}

            //public override string ToString()
            //{
            //    return $"{LåsDatoStart}{LåsDatoEnd}";
            //}
        }

}
