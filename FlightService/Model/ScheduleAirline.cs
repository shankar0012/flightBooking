using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class ScheduleAirline
    {
        [Key]
        public int SchId { get; set; }
        [ForeignKey("Airline")]
        public int AirlineId { get; set; }
        public string FlightNo { get; set; }
        public string FlightName { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ScheduledDays { get; set; }
        public string InstrumentUsed { get; set; }
        public int BCSeats { get; set; }
        public int NBCSeats { get; set; }
        public int TicketCost{ get; set; }
        
        public int Discount { get; set; }
        public string Meals { get; set; }
    }
}
