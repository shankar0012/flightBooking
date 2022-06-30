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

        public int CancelTicket(string PNR)
        {
            var airline = _appDbContext.Booking.FirstOrDefault(z => z.PNR == PNR && z.FromTime.Date<DateTime.Now.Date);
            if (airline != null)
            {
                airline.IsCancel = true;
                airline.ModTime = DateTime.Now;
                _appDbContext.Booking.Update(airline);
                int count = _appDbContext.SaveChanges();
                if (count > 0)
                    return 200;//success
                else return 202;//alreday cancelled
            }
            else
                return 201;//before 24 hours
        }

        public List<Booking> SearchTicket(string PNR, string emailId)
        {
           var airline = new List<Booking>();
            if (PNR != null)
            {
                airline = _appDbContext.Booking.Where(z => z.PNR == PNR && z.IsCancel==false).ToList();
                //var pasengerDetails = _appDbContext.Booking.Where(z => z.PNR == PNR && z.IsCancel == false).SelectMany(row => row.PersonDetails).ToList();
                var pasengerDetails = _appDbContext.Booking.Where(z => z.PNR == PNR && z.IsCancel == false).SelectMany(row => row.PersonDetails).ToList();
                foreach( var itme in airline)
                {
                    itme.PersonDetails = pasengerDetails;
                }

                //var pasengerDetails = _appDbContext.PersonDetails.ToList();
                //foreach( var itme in airline)
                //{
                //    itme.PersonDetails = pasengerDetails.Where(x=>x.Booking.PNRNo==itme.PNRNo).ToList();
                //}

                //airline = null;// _appDbContext.Booking.Where(row => row.PNR == PNR).SelectMany(row => row.PersonDetails).ToList();
                // airline[0].PersonDetails = _appDbContext.Booking.Where(row => row.PNR == PNR).SelectMany(row => row.PersonDetails).ToList();
                // airline = _appDbContext.Booking.Where(m => m.PersonDetails.Any(i => i.PNRno == PNR));
                // var query = meetingsList.Where(m => m.Attendies.Any(i => i.CompanyId == Booking.));
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
            // var airline = _appDbContext.ScheduleAirline.Where(z => z.FromCity.Contains(fromCity) && z.ToCity.Contains(ToCity) && z.FromDate >= date && z.FromDate <= date.AddDays(1)).ToList();
            //var airline = _appDbContext.ScheduleAirline.Join(_appDbContext.Airline,
            //  z => z.AirlineId,
            //  x => x.AirlineId,
            //  (sch, air) =>new { sch,air.IsActive})
            //    .Where(sch => sch.sch.FromCity.Contains(fromCity) && sch.sch.ToCity.Contains(ToCity) && sch.sch.FromDate >= date && sch.sch.FromDate <= date.AddDays(1)&& sch.IsActive==true)
            //  .Select(sch1=>new {sch1.sch }).ToList();
            if (TripType == "OneWay")
            {
                var airline = (from sch in _appDbContext.ScheduleAirline // get person table as p
                               join air in _appDbContext.Airline // implement join as e in EmailAddresses table
                            on sch.AirlineId equals air.AirlineId //implement join on rows where p.BusinessEntityID == e.BusinessEntityID
                               where (sch.FromCity.Contains(fromCity) && sch.ToCity.Contains(ToCity) && sch.FromDate >= date && sch.FromDate <= date.AddDays(1) && air.IsActive == true)
                               select sch).ToList();
                return airline;
            }
            else
            {
                var airline = (from sch in _appDbContext.ScheduleAirline // get person table as p
                               join air in _appDbContext.Airline // implement join as e in EmailAddresses table
                            on sch.AirlineId equals air.AirlineId //implement join on rows where p.BusinessEntityID == e.BusinessEntityID
                               where (sch.FromCity.Contains(ToCity) && sch.ToCity.Contains(fromCity) && sch.FromDate >= date && sch.FromDate <= date.AddDays(1) && air.IsActive == true) ||(sch.FromCity.Contains(fromCity) && sch.ToCity.Contains(ToCity) && sch.FromDate >= date && sch.FromDate <= date.AddDays(1) && air.IsActive == true)
                               select sch).ToList();
                return airline;
            }
           
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

        public IList<Airline> GetActiveAirLinelist()
        {
            var airline = _appDbContext.Airline.Where(x=>x.IsActive==true).ToList();
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
