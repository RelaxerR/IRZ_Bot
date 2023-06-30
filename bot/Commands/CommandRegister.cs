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
    /// Команда для регистрации
    /// </summary>
    class CommandRegister : Command
    {
        public override string Name => Settings.Bot.CommandNames.Register;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            var bu = BotUserController.GetUser(user); // Получаем текущего пользователя
            bu.MyState = BotController.State.FIO; // Устанавливаем состояние пользователя для ввода ФИО
            bu.AddData(0, user.Id.ToString()); // Сохраняем данные о Telegram ID пользователя нового пользоватея
            bu.AddData(1, user.Username); // Сохраняем данные о имени пользователя нового пользоватея
            await BotController.SendMessage(user.Id, // Отправляем сообщение с просьбой ввести ФИО
                Settings.Bot.Messages.WriteFIO,
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
