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
    /// Коанда для перехода в меню настроек
    /// </summary>
    class CommandSettings : Command
    {
        public override string Name => Settings.Bot.CommandNames.Settings;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user, AllowedCommands = new Command[] {
                CommandController.GetCommand (Settings.Bot.CommandNames.Subscribe),
                CommandController.GetCommand (Settings.Bot.CommandNames.Unsubscribe),
                CommandController.GetCommand (Settings.Bot.CommandNames.Menu)
            });
            await BotController.SendMessage(user.Id, // Отправляем сообщение о том, что пользователь находится в меню настроек
                Settings.Bot.Messages.YouInSettings,
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
