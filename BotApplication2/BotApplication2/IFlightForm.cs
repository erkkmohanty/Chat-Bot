using System;
using Microsoft.Bot.Builder.Luis;

namespace BotApplication2
{
    public interface IFlightForm
    {
        string Flight_Source { get; }
        string Flight_Destination { get; set; }
        DateTime Date { get; set; }
        object TicketType { get; set; }
    }
}