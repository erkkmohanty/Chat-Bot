using BotApplication2.DAC;
using BotApplication2.Form;
using BotApplication2.Utilities;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotApplication2.Dialogs
{
    [Serializable]
    public class FlightSearchCustomizedDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var flightSearchDialog = FormDialog.FromForm<FlightSearchFormBasic>(FlightSearchFormBasic.BuildForm, FormOptions.PromptInStart);
            context.Call(flightSearchDialog, this.ResumeFlightSearchAfterHandler);
        }

        private async Task ResumeFlightSearchAfterHandler(IDialogContext context, IAwaitable<FlightSearchFormBasic> result)
        {
            var searchQuery = await result;
            var flights = FlightManager.Flights(searchQuery);
            var response = context.MakeMessage();
            response.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            response.Attachments = AdaptiveCardManager.GetFlightsAdaptiveCards(flights);
            await context.PostAsync(response);
            context.Done<object>(null);

        }
    }
}