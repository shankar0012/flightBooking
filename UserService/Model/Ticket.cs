using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class Ticket
    {
        public string PNR { get; set; }
        public int No_Of_Seats { get; set; }
        public string FullName { get; set; }
        public DateTime BookedDate { get; set; }
    }
}
