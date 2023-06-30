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
    /// Команда для просмотра своих баллов
    /// </summary>
    class CommandGetPoins : Command
    {
        public override string Name => Settings.Bot.CommandNames.GetPoints;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            await BotController.SendMessage(user.Id, // Отправляем сообщение о кол-ве баллов
                Settings.Bot.Messages.MyPoints(BotUserController.GetUser (user).MyPoints),
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
