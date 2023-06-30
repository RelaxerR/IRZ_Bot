/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using TeleBot;
using Core;
using Telegram.Bot.Types;

namespace Commands
{
    /// <summary>
    /// Команда для отписки от рассылки
    /// </summary>
    class CommandUnsubscribed : Command
    {
        public override string Name => Settings.Bot.CommandNames.Unsubscribe;

        public override Command[] AllowedCommands { get; set; }
        public override async void Execute(User user)
        {
            StartExecute(user, new Command[] {
                CommandController.GetCommand (Settings.Bot.CommandNames.Menu)
            });
                BotUserController.GetUser(user).Unsubscribe(); // Отписывем пользователя от рассылки
                await BotController.SendMessage(user.Id, // Уведомляем пользователя, что он отписался от рассылки
                    Settings.Bot.Messages.YouUnsubscribed,
                    BotController.GetKeyboardFromArray(AllowedCommands));
                // Уведомляем Администраторов, что пользователь отписался от рассылки
                await BotController.SendMessageToAdmins(Settings.Bot.Messages.UserUnsubscribed(BotUserController.GetUser(user)));
            
        }
    }
}
