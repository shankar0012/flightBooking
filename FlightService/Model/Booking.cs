using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class Booking
    {
        [Key]
        public int PNRNo { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string PNR { get; set; }
        // [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public string FlightNo { get; set; }
        public string FlightName { get; set; }
        // [ForeignKey("Airline")]
        public int AirlineId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        
        public int NoOfSeats { get; set; }
        public DateTime? AddTime { get; set; } = System.DateTime.Now;
        public DateTime? ModTime { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }
        public string TripType { get; set; }//one way /round trip
        public Int64 TotalAmount { get; set; }
        public bool IsCancel { get; set; } = false;
        public string EmailId { get; set; }
        public List<PersonDetails> PersonDetails { get; set; }
    }
    public class PersonDetails
    {
        [Key]
        public int id { get; set; } 
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MealType { get; set; }
        public string SeatNo { get; set; }
        [ForeignKey("Booking")]
        public int PNRno { get; set; }
        public virtual Booking Booking { get; set; }
       

    }
}
