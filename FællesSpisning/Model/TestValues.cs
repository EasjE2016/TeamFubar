using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FællesSpisning.Model
{
    public class TestValues
    {
        public DateTime EventTime { get; set; }
        public string EventName { get; set; }

        public override string ToString()
        {
            return $"{EventTime} \n {EventName}";
        }
    }

    
}
