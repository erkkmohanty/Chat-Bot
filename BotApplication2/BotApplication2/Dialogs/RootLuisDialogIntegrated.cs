using BotApplication2.DAC;
using BotApplication2.Form;
using BotApplication2.Utilities;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotApplication2.Dialogs
{
    [LuisModel("423ed075-8a02-457a-8778-f4137adc7e94", "e7233e41e92148c4a2c0c9b003d0bf52")]
    [Serializable]
    public class RootLuisDialogIntegrated : LuisDialog<object>
    {

        //For running locally
        public RootLuisDialogIntegrated()
        {
        }
        //public RootLuisDialog() : base(new LuisService(new LuisModelAttribute(
        //   ConfigurationManager.AppSettings["LuisAppId"],
        //   ConfigurationManager.AppSettings["LuisAPIKey"],
        //   domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        //{
        //}


        [LuisIntent("Greetings")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            var queryString = result.Query;
            context.Call(new GreetingDialogLuis(queryString), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("Search Flight")]
        public async Task SearchFlight(IDialogContext context, LuisResult result)
        {
            List<EntityRecommendation> entities = new List<EntityRecommendation>(result.Entities);
            EntityRecommendation dateRecommendation;
            if (result.TryFindEntity("builtin.datetime.date", out dateRecommendation))
            {
                dateRecommendation.Type = "Date";
            }
            if (result.TryFindEntity("builtin.datetimeV2.date", out dateRecommendation))
            {
                dateRecommendation.Type = "Date";
            }
            var flightSearchDialog = new FormDialog<FlightSearchFormBasic>(new FlightSearchFormBasic(),
                FlightSearchFormBasic.BuildForm, FormOptions.PromptInStart,entities);
            context.Call(flightSearchDialog, this.ResumeAfterFlightsFormDialog);
        }

        private async Task ResumeAfterFlightsFormDialog(IDialogContext context, IAwaitable<FlightSearchFormBasic> result)
        {
            try
            {
                var searchQuery = await result;

                var flights = FlightManager.Flights(searchQuery);

                await context.PostAsync($"I found in total {flights.Count()} flights matching your search criteria:");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();
                resultMessage.Attachments = AdaptiveCardManager.GetFlightsAdaptiveCards(flights).ToList();

                await context.PostAsync(resultMessage);

            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation. Quitting from the Flights Search";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }


        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        public async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            var intent = result.TopScoringIntent.Intent;

            await context.PostAsync($"Hi, you are inside {intent} Intent ");
            context.Wait(MessageReceived);
        }

    }
}