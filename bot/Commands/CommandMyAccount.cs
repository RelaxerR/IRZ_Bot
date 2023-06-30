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
    /// Команда для перехода в меню профиля
    /// </summary>
    class CommandMyAccount : Command
    {
        public override string Name => Settings.Bot.CommandNames.MyAccount;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user, new Command[] {
                CommandController.GetCommand (Settings.Bot.CommandNames.ShowData),
                CommandController.GetCommand (Settings.Bot.CommandNames.GetPoints),
                CommandController.GetCommand (Settings.Bot.CommandNames.Menu),
                CommandController.GetCommand (Settings.Bot.CommandNames.BuyMerch)
            });
            await BotController.SendMessage(user.Id, // Отправляем сообщние о том, что пользователь находится в меню своего профиля
                Settings.Bot.Messages.YouInAccount,
                BotController.GetKeyboardFromArray (AllowedCommands));
        }
    }
}
