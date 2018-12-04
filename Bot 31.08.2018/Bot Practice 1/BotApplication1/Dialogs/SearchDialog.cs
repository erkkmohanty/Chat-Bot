using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApplication1.Dialogs
{
    [Serializable]
    public class SearchDialog:IDialog<int>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"You have reached {typeof(SearchDialog).Name}");
        }
    }
}