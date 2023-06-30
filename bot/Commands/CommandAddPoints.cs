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
    /// Команда на добавление баллов пользователю. Только для Администратора
    /// </summary>
    class CommandAddPoints : Command
    {
        public override string Name => Settings.Bot.CommandNames.AddPoints;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            await BotController.SendMessage(user.Id, // Отправляем сообщение с вопросом сколько очков добавить
                Settings.Bot.Messages.AddPoints,
                BotController.GetKeyboardFromArray(AllowedCommands));
            BotUserController.GetUser(user).MyState = BotController.State.AddPoints; // Меняем состояние пользователя для ввода очков
        }
    }
}
