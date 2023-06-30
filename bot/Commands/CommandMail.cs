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
    /// Команда для расслыки сообщения
    /// </summary>
    class CommandMail : Command
    {
        public override string Name => Settings.Bot.CommandNames.Mail;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            Data.GetAllUsersIds();
            StartExecute(user);
            await BotController.SendMessage(user.Id, // Оправляем сообщение с просьой ввести сообщение
                Settings.Bot.Messages.WriteMessage,
                BotController.GetKeyboardFromArray(AllowedCommands));
            BotUserController.GetUser(user).MyState = BotController.State.WriteMessage; // Меняем состояние пользователя ля ввода сообщения
        }
    }
}
