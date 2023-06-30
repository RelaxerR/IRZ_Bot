/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using Core;
using TeleBot;
using Telegram.Bot.Types;

namespace Commands
{
    /// <summary>
    /// Команда для запуска ежедневной игры
    /// </summary>
    class CommandDailyGame : Command
    {
        public override string Name => Settings.Bot.CommandNames.DailyGame;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            // Команда находится в разработке
            StartExecute(user);
            await BotController.SendMessage(user.Id,
                Settings.Bot.Messages.WorkInProgress,
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
