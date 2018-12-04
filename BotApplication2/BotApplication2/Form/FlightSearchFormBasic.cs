using BotApplication2.Utilities;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotApplication2.Form
{
    [Serializable]
    public class FlightSearchFormBasic
    {
        [Prompt("Please enter the source city of your travel")]
        public string Flight_Source { get; set; }
        public string Flight_Destination { get; set; }
        public DateTime Date { get; set; }

        public FlightTicketType? TicketType { get; set; }
        public FlightStopageType? FlightType { get; set; }

        public static IForm<FlightSearchFormBasic> BuildForm()
        {
            return new FormBuilder<FlightSearchFormBasic>()
                    .Message("Welcome to flight search bot!")
                    .Field(nameof(Flight_Source))
                    .Field(nameof(Flight_Destination),
                        validate: async (state, value) =>
                        {
                            var result = new ValidateResult() { IsValid = true, Value = value };
                            if(state.Flight_Source.ToLower()== value.ToString().ToLower())
                            {
                                result.IsValid = false;
                                result.Feedback = "Source and flight destination can't be same ";
                            }
                            return result;
                        }
                    )
                    .AddRemainingFields()
                    .Build();
        }
    }
}