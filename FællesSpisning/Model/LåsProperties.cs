using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    class LåsProperties
    {
        public DateTime LåsDato { get; set; }
        public DateTime DateTimeID { get; set; }

        public LåsProperties()
        {

        }

        public override string ToString()
        {
            return $"{LåsDato}";
        }





    }
}
