using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FællesSpisning.Model
{
    class PlanListe : ObservableCollection<JobPerson>
    {
        public PlanListe() : base()
        {
            this.Add(new JobPerson() { JobPersonNavn = "Kenneth", JobPersonOpgave = "Chefkok" });
        }
    }
}
