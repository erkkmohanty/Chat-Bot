using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotApplication2.Entity
{
    [Serializable]
    public class Flight
    {
        public string ID { get; set; }
        public string Airline_Name { get; set; }
        public int Price { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Arrival_Time { get; set; }

        public string Departue_City_Code { get; set; }
        public string Arrival_City_Code { get; set; }
        public string Departue_City { get; set; }
        public string Arrival_City { get; set; }

        public string Ticket_Type { get; set; }

        public override string ToString()
        {
            return this.Airline_Name.ToUpper() + ": $" + this.Price.ToString();
        }
    }
}