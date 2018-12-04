using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace BotApplication1.Dialogs
{
    public class HelpDialog: IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
           await context.PostAsync("Hello from Help Dialog");
        }
    }
}