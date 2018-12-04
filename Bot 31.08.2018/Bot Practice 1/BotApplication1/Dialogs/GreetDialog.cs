using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApplication1.Dialogs
{
    [Serializable]
    public class GreetDialog : IDialog<object>
    {
        public async  Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity.Text.Contains("hi", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync($"Hi from {typeof(GreetDialog).Name}");
            }
            else if (activity.Text.Contains("hello", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync($"Hello from {typeof(GreetDialog).Name}");
            }
            else
            {
                await context.PostAsync("Hi.......");
            }
            
        }
    }
}