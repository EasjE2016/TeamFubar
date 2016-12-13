using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class JobPerson
    {
        public string JobPersonNavn { get; set; }
        public string JobPersonOpgave { get; set; }
        public DateTime JobDateTime { get; set; }


        public override string ToString()
        {
            return JobPersonNavn + " \n " + JobPersonOpgave + " \n ";
        }
    }
}
