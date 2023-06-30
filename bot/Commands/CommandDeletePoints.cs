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
    /// Команда на списания баллов пользователя
    /// </summary>
    class CommandDeletePoints : Command
    {
        public override string Name => Settings.Bot.CommandNames.DeletePoints;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            await BotController.SendMessage(user.Id, // Отправляем сообщение с просьбой ввести количество баллов для списания
                Settings.Bot.Messages.DeletePoints,
                BotController.GetKeyboardFromArray(AllowedCommands));
            BotUserController.GetUser(user).MyState = BotController.State.DeletePoints; // Меняем состояние пользователя для написания ID и кол-ва баллов
        }
    }
}
