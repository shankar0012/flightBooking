using FlightService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {


        public IAirlineRepository AirRep;
        public FlightController(IAirlineRepository airRep)
        {

            AirRep = airRep;
        }

        //    [HttpGet]
        //    public string test()
        //    {
        //        return "Airline created";
        //    }
        [HttpPost("AddAirLine")]
        public ResponceEntity AddAirLine(Airline airline)
        {
            ResponceEntity re = new ResponceEntity();
            try
            {
                bool insert = AirRep.AddAirline(airline);
                if (insert)
                {
                    re.StatusCode = 200;
                    re.Message = "Airline Added Successfully";
                    return re;

                }
                else
                {
                    re.StatusCode = 201;
                    re.Message = "Airline Alreday Exists";
                    return re;

                }
            }
            catch (Exception ex)
            {
                re.StatusCode = 100;
                re.Message = "Error Occured";
                return re;
            }

        }
        [HttpGet("GetAirLinelist")]
        public IActionResult GetAirLinelist()
        {

            try
            {
                IList<Airline> list = AirRep.GetAirLinelist();
                if (list != null)
                {
                    return Ok(list);

                }
                else
                {
                    return BadRequest("Data Not Found");

                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        [HttpGet("GetActiveAirLinelist")]
        public IActionResult GetActiveAirLinelist()
        {

            try
            {
                IList<Airline> list = AirRep.GetActiveAirLinelist();
                if (list != null)
                {
                    return Ok(list);

                }
                else
                {
                    return BadRequest("Data Not Found");

                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        [HttpPut("UpdateAirLine")]
        public ResponceEntity UpdateAirLine(int airlineId, bool IsActive)
        {
            ResponceEntity re = new ResponceEntity();
            try
            {

                var isUpdate = AirRep.UpdateAirline(airlineId, IsActive);
                if (isUpdate)
                {
                    re.StatusCode = 200;
                    re.Message = "Data Updated Successfully";
                    return re;

                }
                else
                {
                    re.StatusCode = 100;
                    re.Message = "Airline Not Updated Please try again";
                    return re;

                }
            }
            catch (Exception ex)
            {
                re.StatusCode = 100;
                re.Message = "Error Occured";
                return re;
            }
        }

        [HttpPost("ScheduleAirLine")]
        public ResponceEntity ScheduleAirLine(ScheduleAirline sch)
        {
            ResponceEntity re = new ResponceEntity();
            try
            {

                var isUpdate = AirRep.ScheduleAirline(sch);
                if (isUpdate)
                {
                    re.StatusCode = 200;
                    re.Message = "Airline Scheduled Successfully";
                    return re;

                }
                else
                {
                    re.StatusCode = 201;
                    re.Message = "This Flight is already scheduled";
                    return re;

                }
            }
            catch (Exception ex)
            {
                re.StatusCode = 100;
                re.Message = "Error Occured";
                return re;
            }

        }
        [HttpPost("BookTicket")]
        public ResponceEntity BookTicket(Booking booking)
        {
            ResponceEntity re = new ResponceEntity();
            try
            {
                Booking isBooked = AirRep.BookTicket(booking);
            if (isBooked!=null)
            {

                    re.StatusCode = 200;
                    re.Message = "Ticket Booked Successfully with PNR:"+ isBooked.PNR;
                    return re;
                   // return Ok("Ticket Booked Successfully");
            }
            else
            {
                    re.StatusCode = 100;
                    re.Message = "Ticket not Booked Please try again";
                    return re;
                   // return BadRequest("Ticket Booked Please try again");
                }
            }
            catch (Exception ex)
            {
                re.StatusCode = 100;
                re.Message = "Error Occured";
                return re;
            }

        }

        [HttpPut("CancelTicket")]
        public IActionResult CancelTicket(string PNR)
        {

            var isUpdate = AirRep.CancelTicket(PNR);
            if (isUpdate==200)
            {
                // save data into the databse
                return Ok(new { message = "Ticket Cancelled" });
            }
            else if(isUpdate == 202)
            {
                return Ok(new { message = "Ticket already Cancelled" });
            }
            else if (isUpdate == 201)
            {
                return Ok(new { message = "Ticket Cancellation is allowed only before 24 hours of journy date" });
            }
            else
            {
                return Ok(new { message = "Error Occured" });
            }

        }

        [HttpPut("SearchTicket")]
        public IActionResult SearchTicket(string PNR, string emailId)
        {
            try
            {
                var isUpdate = AirRep.SearchTicket(PNR, emailId);
                if (isUpdate.Count > 0)
                {

                    return Ok(isUpdate);
                }
                else
                {
                    return Ok(new { message = "Date not found" });
                }
            }
            catch(Exception ex)
            {
                return Ok(new { message = "Error Occured" });
            }

        }


        [HttpPut("SearchFlight")]
        public IActionResult SearchFlight(string fromCity, string ToCity,DateTime dateTime,string TripType)
        {


            var list = AirRep.SearchFlight(fromCity, ToCity, dateTime, TripType);
            if (list.Count>0)
            {
                // save data into the databse
                return Ok(list);
            }
            else
            {
                return Ok(new {message = "Data Not Found"});
            }

        }

        [HttpGet("GetFlightDetails")]
        public IActionResult GetFlightDetails(int schId)
        {


            var FlightDetails = AirRep.GetFlightDetails(schId);
            if (FlightDetails != null)
            {
                // save data into the databse
                return Ok(FlightDetails);
            }
            else
            {
                return BadRequest("Data Not Found");
            }

        }


        [HttpGet("test")]
        public IActionResult test()
        {


            var list = AirRep.SearchFlight("Pune", "hyd",DateTime.Now,"OneWay");
            if (list != null)
            {
                // save data into the databse
                return Ok(list);
            }
            else
            {
                return BadRequest("Data Not Found");
            }

        }
    }
}
