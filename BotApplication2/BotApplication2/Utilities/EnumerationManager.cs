using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotApplication2.Utilities
{
    public enum FlightTicketType
    {
        Economy = 1,
        Business,
        FirstClass
    }
    public enum FlightStopageType
    {
        Direct = 1,
        NonStop,
        Connecting
    }
}