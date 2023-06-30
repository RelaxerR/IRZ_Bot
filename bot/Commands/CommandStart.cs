/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using Core;
using Telegram.Bot.Types;
using TeleBot;

namespace Commands
{
    class CommandStart : Core.Command
    {
        public override string Name => Core.Settings.Bot.CommandNames.Start;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            /*
             * Если ользователь - Администратор - клавиатура Админа
             * Иначе если зарегистрирован - клавиатура меню
             * Иначе - клавиатура только с кнопкой регистрации
             */
            if (IsAdmin(user))
            {
                StartExecute(user, AdminCommands);
            }
            else
            {
                if (BotUserController.GetUser(user).IsRegistred())
                    StartExecute(user, MenuCommands);
                else
                    StartExecute(user, new Command[] {
                        CommandController.GetCommand (Settings.Bot.CommandNames.Register)
                    });
                await BotController.SendMessage(user.Id, // Отправяем приветственное сообщение
                Settings.Bot.Messages.Hello,
                BotController.GetKeyboardFromArray(AllowedCommands));
            }

            
        }
    }
}
