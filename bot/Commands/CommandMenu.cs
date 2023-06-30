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
    /// Команда для перехода в главное меню
    /// </summary>
    class CommandMenu : Command
    {
        public override string Name => Settings.Bot.CommandNames.Menu;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            // Если пользователь - Администратор, устаавливаем достуные команды Администратора. Иначе - для обычного пользователя
            if (IsAdmin(user))
                StartExecute(user, AdminCommands);
            else
                if (BotUserController.GetUser(user).IsRegistred())
                StartExecute(user, MenuCommands);
            else
                StartExecute(user, new Command[] { new CommandRegister() });

            await BotController.SendMessage(user.Id, // Отправляем сообщение о том, что пользователь находится в меню
                Settings.Bot.Messages.YouInMenu,
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
