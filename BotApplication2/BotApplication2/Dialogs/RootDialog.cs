using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApplication2.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;
            string textToMatch = activity.Text.ToLower();
            if (textToMatch.Contains("hi") || textToMatch.Contains("hello"))
            {
                await context.Forward(new GreetingDialog(), ResumeAfterHandler, activity);
               
            }
            else if (textToMatch.Contains("search") || textToMatch.Contains("find") || textToMatch.Contains("flights"))
            {
                context.Call(new FlightSearchCustomizedDialog(), ResumeAfterHandler);
                //await context.Forward(new FlightSearchBasicDialog(), ResumeAfterHandler, activity);
               // context.Call(new FlightSearchDialogCustomizedRich(), resumeAfterHandler);
            }
            else
            {
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
                context.Wait(MessageReceivedAsync);
            }

        }
            // Return our reply to the user

        private async Task ResumeAfterHandler(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }
    }
}