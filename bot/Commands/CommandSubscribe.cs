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
    /// Команда для подписки на рассылку
    /// </summary>
    class CommandSubscribe : Command
    {
        public override string Name => Settings.Bot.CommandNames.Subscribe;

        public override Command[] AllowedCommands { get; set; }
        public override async void Execute(User user)
        {
            if (BotUserController.GetUser(user).tmp == false)
            {
                BotUserController.GetUser(user).AddPoints(10, Settings.Achivements.Subscribe);
                BotUserController.GetUser(user).tmp = true;
            }
            StartExecute(user, new Command[] {
                CommandController.GetCommand (Settings.Bot.CommandNames.Menu)
            });
            BotUserController.GetUser(user).Subscribe(); // Подписывем пользователя на рассылку
            await BotController.SendMessage(user.Id, // Сообщаем пользователю, что он подписался на рассылку
                  Settings.Bot.Messages.YouSubscribed,
                  BotController.GetKeyboardFromArray(AllowedCommands));
            // Уведомляем Администраторов, что пользователь подпиан
            await BotController.SendMessageToAdmins(Settings.Bot.Messages.UserSubscribed(BotUserController.GetUser(user)));
        }

    }

}
