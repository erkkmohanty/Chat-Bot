using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotApplication2.Dialogs
{
    [LuisModel("423ed075-8a02-457a-8778-f4137adc7e94", "e7233e41e92148c4a2c0c9b003d0bf52")]
    [Serializable]
    public class RootLuisDialogBasic : LuisDialog<object>
    {

        //For running locally
        public RootLuisDialogBasic()
        {
        }
        //public RootLuisDialog() : base(new LuisService(new LuisModelAttribute(
        //   ConfigurationManager.AppSettings["LuisAppId"],
        //   ConfigurationManager.AppSettings["LuisAPIKey"],
        //   domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        //{
        //}


        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);

        }

        [LuisIntent("SearchFlight")]
        public async Task SearchFlight(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
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