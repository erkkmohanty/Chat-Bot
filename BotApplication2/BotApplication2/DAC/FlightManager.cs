using BotApplication2.Entity;
using BotApplication2.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotApplication2.DAC
{
    public static class FlightManager
    {
        public static IEnumerable<Flight> Flights(FlightSearchFormBasic searchQuery)
        {
            var flights = new List<Flight>();
            var random = new Random();

            //filling data manually for demo purpose
            for (int i = 0; i < random.Next(2, 5); i++)
            {
                Flight flight = new Flight()
                {
                    Airline_Name = i % 2 == 0 ? "Jet Airways" : "Emirates",
                    ID = random.Next(300, 700).ToString(),
                    Price = random.Next(400, 700),
                    Departue_City = searchQuery.Flight_Source,
                    Arrival_City = searchQuery.Flight_Destination,
                    Arrival_City_Code = searchQuery.Flight_Destination.Substring(0, 3).ToUpper(),
                    Departue_City_Code = searchQuery.Flight_Source.Substring(0, 3).ToUpper(),
                    Arrival_Time = searchQuery.Date.AddHours(random.Next(4, 10)),
                    Departure_Time = searchQuery.Date.AddHours(-(random.Next(0, 3))),
                    Ticket_Type = searchQuery.TicketType.ToString()

                };
                flights.Add(flight);
            }


            return flights;
        }
    }
}