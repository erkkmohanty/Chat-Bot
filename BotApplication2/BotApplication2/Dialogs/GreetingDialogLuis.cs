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
    public class GreetingDialogLuis : IDialog<object>
    {
        string queryString;
        public GreetingDialogLuis(string query)
        {
            this.queryString = query;
        }


        public async Task StartAsync(IDialogContext context)
        {
            string textToMatch = this.queryString.ToLower();
            string returnString = String.Empty;

            if (textToMatch.Contains("hi") || textToMatch.Contains("hello"))
            {
                returnString = $"Hi , good to see you.";
            }
            else if (textToMatch.Contains("morning"))
            {
                returnString = $"Morning , what a pleasant day it is.";
            }
            else if (textToMatch.Contains("adios"))
            {
                returnString = $"adios amigo.";
            }
            else
            {
                returnString = $"Hi, good to see you.";
            }
            await context.PostAsync(returnString);
            context.Done<string>("Greeting dialog done");



        }
    }
}