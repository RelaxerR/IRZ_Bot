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
    /// Команда для просмотра анкеты пользователя
    /// </summary>
    class CommandShowData : Command
    {
        public override string Name => Settings.Bot.CommandNames.ShowData;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            await BotController.SendMessage(user.Id, // Отправляем сообщение с данными анкеты
                BotUserController.GetUser(user).GetData(),
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
