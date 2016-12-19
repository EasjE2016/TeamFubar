using FællesSpisning.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FællesSpisning.ViewModel
{
    class KasserViewModel : INotifyPropertyChanged
    {

        //RelayCommands
        public RelayCommand AddToBeregningCommand { get; set; }
        public RelayCommand RemoveBeregningCommand { get; set; }
        public RelayCommand UdregnKuvertPrisCommand { get; set; }
        public RelayCommand UdregnEnkeltHusstandPrisCommand { get; set; }

        public ObsHusListeSingleton HusListeSingleton { get; set; }
        public PlanlægningSingleton PlanSingleton { get; set; }

        private DateTime _kassereDateTime = DateTime.Today;
        public DateTime KassereDateTime
        {
            get { return _kassereDateTime; }
            set
            {
                _kassereDateTime = value.Date;
            }
        }

        private ObservableCollection<Hus> _husListe;
        public ObservableCollection<Hus> HusListe
        {
            get { return _husListe; }
            set
            {
                _husListe = value;
                OnPropertyChanged(nameof(HusListe));
            }
        }

        private ObservableCollection<Hus> _resultatHusListe;
        public ObservableCollection<Hus> ResultatHusListe
        {
            get { return _resultatHusListe; }
            set { _resultatHusListe = value;
                OnPropertyChanged(nameof(ResultatHusListe));
            }
        }

        private int _resultatHusListeSelectedIndex;
        public int ResultatHusListeSelectedIndex
        {
            get { return _resultatHusListeSelectedIndex; }
            set { _resultatHusListeSelectedIndex = value;
                OnPropertyChanged(nameof(ResultatHusListeSelectedIndex));
            }
        }


        private ObservableCollection<Udlæg> _udlægList;
        public ObservableCollection<Udlæg> UdlægList
        {
            get { return _udlægList; }
            set { _udlægList = value; }
        }

        private Udlæg _selectedUdlæg;
        public Udlæg SelectedUdlæg
        {
            get { return _selectedUdlæg; }
            set { _selectedUdlæg = value;
                OnPropertyChanged(nameof(SelectedUdlæg));
            }
        }

        public List<Hus> RelevanteHusstande { get; set; }

        private String _udlagtSumString;
        public String UdlagtSumString
        {
            get { return _udlagtSumString; }
            set { _udlagtSumString = value;
                OnPropertyChanged(nameof(UdlagtSumString));
            }
        }

        private double _udlagtSum;
        public double UdlagtSum
        {
            get { return _udlagtSum; }
            set { _udlagtSum = value; }
        }

        private double _finalSum;
        public double FinalSum
        {

            get { return _finalSum; }
            set { _finalSum = value;
                OnPropertyChanged(nameof(FinalSum));
            }
        }

        private String _finalSumString;
        public String FinalSumString
        {
            get { return _finalSumString; }
            set { _finalSumString = value;
                OnPropertyChanged(nameof(FinalSumString));
            }
        }

        private String _finalSumString2;
        public String FinalSumString2
        {
            get { return _finalSumString2; }
            set { _finalSumString2 = value;
                OnPropertyChanged(nameof(FinalSumString2));
            }
        }



        private double _enkeltHusSum;
        public double EnkeltHusSum
        {
            get { return _enkeltHusSum; }
            set { _enkeltHusSum = value;
                OnPropertyChanged(nameof(EnkeltHusSum));
                }
        }

        private String _enkeltHusSumString;
        public String EnkeltHusSumString
        {
            get { return _enkeltHusSumString; }
            set { _enkeltHusSumString = value;
                OnPropertyChanged(nameof(EnkeltHusSumString));
            }
        }

        

        private double _divisonModfier;
        public double DivisonModifier
        {
            get { return _divisonModfier; }
            set { _divisonModfier = value; }
        }

        private double _multiplicationEnkeltHusstand;
        public double MultiplicationModifierEnkeltHusstand
        {
            get { return _multiplicationEnkeltHusstand; }
            set { _multiplicationEnkeltHusstand = value; }
        }


        public KasserViewModel()
        {

            HusListeSingleton = ObsHusListeSingleton.Instance;
            HusListe = HusListeSingleton.HusListe;

            PlanSingleton = PlanlægningSingleton.Instance;

            UdlægList = new ObservableCollection<Udlæg>();
            RelevanteHusstande = new List<Hus>();
            ResultatHusListe = new ObservableCollection<Hus>();
            ResultatHusListe.Add(new Hus(0, 0, 0, 0, 0));
            SelectedUdlæg = new Udlæg();

            AddToBeregningCommand = new RelayCommand(AddToBeregning, null);
            RemoveBeregningCommand = new RelayCommand(RemoveBeregning, null);
            UdregnKuvertPrisCommand = new RelayCommand(KuvertUdregning, null);
            UdregnEnkeltHusstandPrisCommand = new RelayCommand(EnkeltHusstandUdregning ,null);

            EnkeltHusSumString = $"Husstand {ResultatHusListeSelectedIndex}\r\nskal betale:\r\n{EnkeltHusSum} kr.";
        }

        public void AddToBeregning()
        {
            if (!String.IsNullOrWhiteSpace(UdlagtSumString))
            {
                Udlæg tempUdlæg = new Udlæg();
                

                if (double.TryParse(UdlagtSumString, out _udlagtSum))
                {
                    if (UdlagtSumString.Contains(","))
                    {
                        char[] tempArray = UdlagtSumString.ToCharArray();
                        int i = 0;
                        foreach (Char x in tempArray)
                        {
                            if (x == ',')
                            {
                                tempArray[tempArray.ToList().FindIndex(c => c == x)] = '.';
                                i++;

                            }
                        }

                        if (i <= 1)
                        {
                            tempUdlæg.UdlagtSum = double.Parse(UdlagtSumString = new string(tempArray));
                            tempUdlæg.UdlagtSum = Math.Round(tempUdlæg.UdlagtSum, 2);
                            tempUdlæg.DatoForUdlæg = KassereDateTime;

                            foreach (Hus husObj in HusListe)
                            {
                                if (husObj == HusListe[HusListeSingleton.SelectedIndex])
                                {
                                    tempUdlæg.HusNr = husObj.HusNr;
                                }
                            }

                            UdlægList.Add(tempUdlæg);
                        } else
                        {
                            MessageDialog noEvent = new MessageDialog("Du kan kun bruge 1 komma.");
                            noEvent.Commands.Add(new UICommand { Label = "Ok" });
                            noEvent.ShowAsync().AsTask();
                        }
                    } else {                    
                    
                        tempUdlæg.UdlagtSum = UdlagtSum;
                        tempUdlæg.UdlagtSum = Math.Round(tempUdlæg.UdlagtSum, 2);
                        tempUdlæg.DatoForUdlæg = KassereDateTime;

                        foreach (Hus husObj in HusListe)
                        {
                            if (husObj == HusListe[HusListeSingleton.SelectedIndex])
                            {
                                tempUdlæg.HusNr = husObj.HusNr;
                            }
                        }

                        UdlægList.Add(tempUdlæg);
                    
                   }
                }

                else
                {
                    MessageDialog noEvent = new MessageDialog("Du kan kun indtaste tal!");
                    noEvent.Commands.Add(new UICommand { Label = "Ok" });
                    noEvent.ShowAsync().AsTask();
                }
            }
            
        }//Method close

        public void RemoveBeregning()
        {
            if(SelectedUdlæg != null)
            {
                UdlægList.Remove(SelectedUdlæg);
            }
        }

        public void LavResultatHusLise()
        {
            ResultatHusListe.RemoveAt(0);

            foreach (Hus relHus in RelevanteHusstande)
            {
                if((ResultatHusListe.Count == 0) || (ResultatHusListe.Any(resultHus => resultHus.HusNr == relHus.HusNr) == false))
                {
                    ResultatHusListe.Add(relHus);
                }
            }

            if (ResultatHusListe.Count > 0)
            {
                ResultatHusListeSelectedIndex = 0;

            }
        
        }

        public void KuvertUdregning()
        {
        if(UdlægList.Count > 0) { 
            RelevanteHusstande.Clear();
            FinalSum = 0;
            DivisonModifier = 0;

            foreach (Udlæg udlægObj in UdlægList)
            {
                FinalSum += udlægObj.UdlagtSum;

                foreach (Hus tilmeldObj in PlanSingleton.TilmeldsListe)
                {

                    if (udlægObj.DatoForUdlæg == tilmeldObj.DT)
                    {
                        RelevanteHusstande.Add(tilmeldObj);
                    }
                }
            }

            foreach (Hus relHusObj in RelevanteHusstande)
            {
                DivisonModifier += relHusObj.AntalVoksne * 1;
                DivisonModifier += relHusObj.AntalUnge * 0.5;
                DivisonModifier += relHusObj.AntalBørn * 0.25;
            }

            FinalSum = Math.Round(FinalSum / DivisonModifier, 2);
            
            FinalSumString = $"Kuvertspris:\r\n{FinalSum} kr.";
            FinalSumString2 = FinalSum.ToString();

            LavResultatHusLise();
          }
          else
          {
                MessageDialog noEvent = new MessageDialog("Du kan ikke udregne med en tom liste!");
                noEvent.Commands.Add(new UICommand { Label = "Ok" });
                noEvent.ShowAsync().AsTask();
           }
       }

        public void EnkeltHusstandUdregning()
        {
            if (!String.IsNullOrWhiteSpace(FinalSumString2))
            {
                if (double.TryParse(FinalSumString2, out _finalSum))
                {
                    if (FinalSumString2.Contains(","))
                    {
                        char[] tempArray = FinalSumString2.ToCharArray();
                        int i = 0;
                        foreach (Char x in tempArray)
                        {
                            if (x == ',')
                            {
                                tempArray[tempArray.ToList().FindIndex(c => c == x)] = '.';
                                i++;

                            }
                        }

                        if (i <= 1)
                        {
                            FinalSum = double.Parse(FinalSumString2 = new string(tempArray));
                            EnkeltHusSum = 0;
                            MultiplicationModifierEnkeltHusstand = 0;

                            MultiplicationModifierEnkeltHusstand += ResultatHusListe[ResultatHusListeSelectedIndex].AntalVoksne * 1;
                            MultiplicationModifierEnkeltHusstand += ResultatHusListe[ResultatHusListeSelectedIndex].AntalUnge * 0.5;
                            MultiplicationModifierEnkeltHusstand += ResultatHusListe[ResultatHusListeSelectedIndex].AntalBørn * 0.25;

                            EnkeltHusSum = FinalSum * MultiplicationModifierEnkeltHusstand;

                            int AttendanceTimes = 0;

                            foreach (Hus relHusObj in RelevanteHusstande)
                            {
                                if (relHusObj.HusNr == ResultatHusListe[ResultatHusListeSelectedIndex].HusNr)
                                {
                                    AttendanceTimes++;
                                }
                            }

                            foreach (Udlæg udlægObj in UdlægList)
                            {
                                if (udlægObj.HusNr == ResultatHusListe[ResultatHusListeSelectedIndex].HusNr)
                                {
                                    AttendanceTimes--;
                                }
                            }

                            if (AttendanceTimes <= 0)
                            {
                                EnkeltHusSum = 0;
                                EnkeltHusSumString = $"Husstand {ResultatHusListe[ResultatHusListeSelectedIndex].HusNr}\r\nskal betale:\r\n{EnkeltHusSum} kr.";
                            }
                            else
                            {
                                EnkeltHusSum = Math.Round(EnkeltHusSum * AttendanceTimes, 2);
                                EnkeltHusSumString = $"Husstand {ResultatHusListe[ResultatHusListeSelectedIndex].HusNr}\r\nskal betale:\r\n{EnkeltHusSum} kr.";
                            }
                        }
                        else
                        {
                            MessageDialog noEvent = new MessageDialog("Du kan kun bruge 1 komma.");
                            noEvent.Commands.Add(new UICommand { Label = "Ok" });
                            noEvent.ShowAsync().AsTask();
                        }
                    }
                    else
                    {

                        EnkeltHusSum = 0;
                        MultiplicationModifierEnkeltHusstand = 0;

                        MultiplicationModifierEnkeltHusstand += ResultatHusListe[ResultatHusListeSelectedIndex].AntalVoksne * 1;
                        MultiplicationModifierEnkeltHusstand += ResultatHusListe[ResultatHusListeSelectedIndex].AntalUnge * 0.5;
                        MultiplicationModifierEnkeltHusstand += ResultatHusListe[ResultatHusListeSelectedIndex].AntalBørn * 0.25;

                        EnkeltHusSum = FinalSum * MultiplicationModifierEnkeltHusstand;

                        int AttendanceTimes = 0;

                        foreach (Hus relHusObj in RelevanteHusstande)
                        {
                            if (relHusObj.HusNr == ResultatHusListe[ResultatHusListeSelectedIndex].HusNr)
                            {
                                AttendanceTimes++;
                            }
                        }

                        foreach (Udlæg udlægObj in UdlægList)
                        {
                            if (udlægObj.HusNr == ResultatHusListe[ResultatHusListeSelectedIndex].HusNr)
                            {
                                AttendanceTimes--;
                            }
                        }

                        if (AttendanceTimes <= 0)
                        {
                            EnkeltHusSum = 0;
                            EnkeltHusSumString = $"Husstand {ResultatHusListe[ResultatHusListeSelectedIndex].HusNr}\r\nskal betale:\r\n{EnkeltHusSum} kr.";
                        }
                        else
                        {
                            EnkeltHusSum = EnkeltHusSum * AttendanceTimes;
                            EnkeltHusSumString = $"Husstand {ResultatHusListe[ResultatHusListeSelectedIndex].HusNr}\r\nskal betale:\r\n{EnkeltHusSum} kr.";
                        }

                    }
                }

                else
                {
                    MessageDialog noEvent = new MessageDialog("Du kan kun indtaste tal!");
                    noEvent.Commands.Add(new UICommand { Label = "Ok" });
                    noEvent.ShowAsync().AsTask();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        }
    }
}

