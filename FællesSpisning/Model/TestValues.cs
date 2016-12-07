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

        public bool Equals(TestValues objtest)
        {
            return objtest.EventTime.Equals(EventTime);
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
                      

            
            return Equals((TestValues) obj);
        }

        public override string ToString()
        {
            return $"{EventTime} \n {EventName}";
        }
    }

    
}
