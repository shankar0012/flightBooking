using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
  public  interface IAirlineRepository
    {
        bool AddAirline(Airline airline);
        bool UpdateAirline(int AirlineId, bool IsActive);
        // Airline GetAirlineById(int AirlineId);
        bool ScheduleAirline(ScheduleAirline schAir);
        Booking BookTicket(Booking booking);
        int CancelTicket(string PNR);
        List<Booking> SearchTicket(string PNR,string emailId);

        IList<ScheduleAirline> SearchFlight(string fromCity, string ToCity,DateTime date,string TripType);
        IList<Airline> GetAirLinelist();
        IList<Airline> GetActiveAirLinelist();
        ScheduleAirline GetFlightDetails(int Flightno);
    }
}
