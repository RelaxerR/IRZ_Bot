/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using Core;
using Telegram.Bot.Types;
using TeleBot;

namespace Commands
{
    /// <summary>
    /// Команда для просмотра текущих событий
    /// </summary>
    class CommandEvents : Command
    {
        public override string Name => Settings.Bot.CommandNames.Events;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            // Команда находится в разработке
            StartExecute(user);
            await BotController.SendMessage(user.Id,
                Settings.Bot.Messages.Kostil,
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
