using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApplication1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string FlightsOption = "Flights";

        private const string HotelsOption = "Hotels";
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

            if (activity.Text.Contains("Hi", StringComparison.InvariantCultureIgnoreCase) || activity.Text.Contains("Hello", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.Forward(new GreetDialog(), ResumeHandler, activity);
            }
            else if (activity.Text.Contains("Search", StringComparison.InvariantCultureIgnoreCase))
            {
                //await context.Forward(new SearchDialog(), ResumeHandler, activity);
                ShowOptions(context);
            }
            else if (activity.Text.ToLower().Contains("help") || activity.Text.ToLower().Contains("support") || activity.Text.ToLower().Contains("problem"))
            {
               await context.Forward(new SearchDialog(), ResumeAfterSupportDialog, activity, CancellationToken.None);
            }
            else
            {
                // Return our reply to the user
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
                context.Wait(MessageReceivedAsync);
            }
            //context.Done("Completed");

        }

        private async Task ResumeHandler(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }
        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { FlightsOption, HotelsOption }, "Are you looking for a flight or a hotel?", "Not a valid option", 3);
        }
        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case FlightsOption:
                        context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case HotelsOption:
                        context.Call(new HelpDialog(), this.ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attempts :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }
        private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticketNumber = await result;

            await context.PostAsync($"Thanks for contacting our support team. Your ticket number is {ticketNumber}.");
            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}