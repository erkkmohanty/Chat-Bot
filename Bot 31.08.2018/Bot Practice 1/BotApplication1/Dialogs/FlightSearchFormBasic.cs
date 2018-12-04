using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace BotApplication1.Dialogs
{
    public class FlightSearchFormBasic
    {
        [Prompt("Please enter the source city of your travel")]
        public string Flight_Source { get; set; }
        public string Flight_Destination { get; set; }
        public DateTime Date { get; set; }

        public FlightTicketType? TicketType { get; set; }

        public  FlightStopageType? FlightType { get; set; }

        public static IForm<FlightSearchFormBasic> BuildForm()
        {
            return  new FormBuilder<FlightSearchFormBasic>()
                .Message("Welcome to flight search bot!").Build();
        }
    }

    public enum FlightTicketType
    {

    }

    public enum FlightStopageType
    {

    }
}