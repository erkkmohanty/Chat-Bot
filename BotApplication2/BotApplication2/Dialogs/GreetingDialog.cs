using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BotApplication2.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            string textToMatch = activity.Text.ToLower();
            if (textToMatch.Contains("hi"))
            {
                await context.PostAsync("Hi, good to see you !!");
            }
            else if (textToMatch.Contains("hello"))
            {
                await context.PostAsync("Hello, glad to see you !!");
            }
            else if (textToMatch.Contains("morning"))
            {

                await context.PostAsync("Morning, what a pleasant day it is !!");
            }
            else if (textToMatch.Contains("adios"))
            {
                await context.PostAsync("adios amigo");
            }
            else await context.PostAsync($"Hi, good to see you !!");
           context.Done<string>("Greeting dialog done");

        }
    }
}