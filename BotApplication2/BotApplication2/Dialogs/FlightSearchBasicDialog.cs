using BotApplication2.Form;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotApplication2.Dialogs
{
    [Serializable]
    public class FlightSearchBasicDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var flightSearchDialog = FormDialog.FromForm<FlightSearchFormBasic>(FlightSearchFormBasic.BuildForm, FormOptions.PromptInStart);
            context.Call(flightSearchDialog, this.ResumeFlightSearchAfterHandler);
        }

        private async Task ResumeFlightSearchAfterHandler(IDialogContext context, IAwaitable<FlightSearchFormBasic> result)
        {
            await context.PostAsync("We have found 5 flights ");
            context.Done<object>(null);
        }
    }
}