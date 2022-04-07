using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeShiftTracker.Models
{
    internal class Shift
    {
        public long shiftId { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public decimal pay { get; set; }
        public decimal minutes { get; set; }
        public string location { get; set; }
    }
}
