using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class LåstDates
    {

        public DateTime LåsDato { get; set; }
        public DateTime DateTimeID { get; set; }

        public LåstDates()
        {

        }

        public override string ToString()
        {
            return $"{LåsDato}";
        }

    }
}
