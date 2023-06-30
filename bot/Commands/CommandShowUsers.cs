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
    /// Комнда для просмотра зарегистрированных пользователей
    /// </summary>
    class CommandShowUsers : Command
    {
        public override string Name => Settings.Bot.CommandNames.ShowUsers;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            Data.GetAllUsersIds();
            StartExecute(user);
            // Создаем текст для вывода
            foreach (BotUser u in BotUserController.TrenerUsers)
            {
                await BotController.SendMessage(user.Id, // Отправляем сообщение о пользователе
                $"{u.GetData()}",
                BotController.GetKeyboardFromArray(AllowedCommands));
            }
        }
    }
}
