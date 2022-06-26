using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService
{
    public class Airline
    {
        [Key]
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }

        public DateTime? Addtime { get; set; } = DateTime.Now;
        public DateTime? Modtime { get; set; }
        public bool IsActive { get; set; }
       

    }
}
