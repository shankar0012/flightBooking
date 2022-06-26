using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class SqlAirlineRepository : IAirlineRepository
    {
        private readonly AppDbContext _appDbContext;

        public SqlAirlineRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool AddAirline(Airline airline)
        {
            var IsExists = _appDbContext.Airline.Any(x => x.AirlineName == airline.AirlineName);

            if (IsExists)
            {
                return false;
            }
            else
            {
                _appDbContext.Airline.Add(airline);
                _appDbContext.SaveChanges();
                return true;
            }
        }


        //public UserModel GetRegisterUser(UserModel user)
        //{
        //    UserModel usr = _appDbContext.UserTable.Where(x => x.Username == user.Username).FirstOrDefault();
        //    return usr;


        //}

        public bool UpdateAirline(int Id, bool IsActive)
        {
            var airline = _appDbContext.Airline.FirstOrDefault(z => z.AirlineId == Id);
            if (airline != null)
            {
                airline.IsActive = IsActive;
                airline.Modtime = DateTime.Now;
                _appDbContext.Airline.Update(airline);
                int count = _appDbContext.SaveChanges();
                if (count > 0)
                    return true;
                else return false;
            }
            else
                return false;
        }
        public bool ScheduleAirline(ScheduleAirline airline)
        {
            var IsExists = _appDbContext.ScheduleAirline.Any(x => x.FlightNo == airline.FlightNo && x.FromDate >= airline.FromDate && x.ToDate <= airline.ToDate);

            if (IsExists)
            {
                return false;
            }
            else
            {
                _appDbContext.ScheduleAirline.Add(airline);
                //  var airline1 = _appDbContext.ScheduleAirline.FirstOrDefault(x=>x.AirlineId== air.AirlineId);
                _appDbContext.SaveChanges();
                return true;
            }

        }

        public Booking BookTicket(Booking booking)
        {
            var b = _appDbContext.Booking.Add(booking);
            _appDbContext.SaveChanges();
            return b.Entity;

        }

        public bool CancelTicket(string PNR)
        {
            var airline = _appDbContext.Booking.FirstOrDefault(z => z.PNR == PNR);
            if (airline != null)
            {
                airline.IsCancel = true;
                airline.ModTime = DateTime.Now;
                _appDbContext.Booking.Update(airline);
                int count = _appDbContext.SaveChanges();
                if (count > 0)
                    return true;
                else return false;
            }
            else
                return false;
        }

        public IList<Booking> SearchTicket(string PNR, string emailId)
        {
            var airline = new List<Booking>();
            if (PNR != null)
            {
                airline = _appDbContext.Booking.Where(z => z.PNR == PNR && z.IsCancel==false).ToList();
            }
            else
            {
                airline = _appDbContext.Booking.Where(z => z.EmailId == emailId && z.IsCancel == false).ToList();
            }
            if (airline != null)
            {

                return airline;
            }
            else
                return null;
        }

        public IList<ScheduleAirline> SearchFlight(string fromCity, string ToCity, DateTime date, string TripType)
        {
            var airline = _appDbContext.ScheduleAirline.Where(z => z.FromCity.Contains(fromCity) && z.ToCity.Contains(ToCity) && z.FromDate >= date && z.FromDate <= date.AddDays(1)).ToList();
            if (airline != null)
            {

                return airline;
            }
            else
                return null;
        }

        public IList<Airline> GetAirLinelist()
        {
            var airline = _appDbContext.Airline.ToList();
            if (airline != null)
            {

                return airline;
            }
            else
                return null;
        }

        public ScheduleAirline GetFlightDetails(int schId)
        {

            var ScheduleAirline = _appDbContext.ScheduleAirline.Where(z => z.SchId == schId).FirstOrDefault();
            if (ScheduleAirline != null)
            {

                return ScheduleAirline;
            }
            else
                return null;
        }

        
    }
}
