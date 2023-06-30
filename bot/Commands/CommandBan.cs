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
    /// Команда на удаление данных пользователя. Только для Администраторов
    /// </summary>
    class CommandBan : Command
    {
        public override string Name => Settings.Bot.CommandNames.Ban;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user); 
            await BotController.SendMessage(user.Id, // Оправляем сообщение с просьбой вести ID ползователя, данные которого нужно удалить
                Settings.Bot.Messages.WriteUserId,
                BotController.GetKeyboardFromArray(AllowedCommands));
            BotUserController.GetUser(user).MyState = BotController.State.Ban; // Меняем состояниие пользователя на ввод ID
        }
    }
}