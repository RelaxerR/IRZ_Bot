/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using Telegram.Bot.Types;
using System.Linq;
using TeleBot;

namespace Core
{
    /// <summary>
    /// Абстрактый класс Команды. Все команды наследовать от этого класса
    /// </summary>
    abstract class Command
    {
        /// <summary>
        /// Имя команды для пльзователя. Добвлять из Settings.Bot.CommandNames
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Допустимые для пользователя команды после выполнения этой команды. Инициализировать массив в Execute
        /// </summary>
        public abstract Command[] AllowedCommands { get; set; }
        /// <summary>
        /// Выполнить команду для пользователя
        /// </summary>
        /// <param name="user">Пользователь Телеграма</param>
        public abstract void Execute (User user);
        
        public Command () {
            Debug.Log ($"Command {Name} initialized!", "Command");
        }

        /// <summary>
        /// Запускать этот метод для каждой команды!
        /// </summary>
        /// <param name="user">Пользователь Телеграма</param>
        /// <param name="cmds">Допутимые команды после выполнения этой. Если не указывать, добавляется только команда перехода в главное меню</param>
        protected void StartExecute (User user, Command[] cmds = null) {
            if (cmds == null)
                AllowedCommands = new Command[]
                {
                    CommandController.GetCommand(Settings.Bot.CommandNames.Menu)
                };
            else
                AllowedCommands = cmds;
            BotUserController.GetUser(user).AllowedCommands = AllowedCommands.ToList();
            Debug.Log ($"Executing {Name}...\n" +
                $"Allowed commands length: {AllowedCommands.Length}", this);
        }

        protected bool IsAdmin(User user) => Settings.Bot.AdminsId.Contains(user.Id);

        protected static Command[] AdminCommands = new Command[] {
            CommandController.GetCommand(Settings.Bot.CommandNames.AddPoints),
            CommandController.GetCommand(Settings.Bot.CommandNames.ShowUsers),
            CommandController.GetCommand(Settings.Bot.CommandNames.DeletePoints),
            CommandController.GetCommand(Settings.Bot.CommandNames.Mail),
            CommandController.GetCommand(Settings.Bot.CommandNames.Ban),
            CommandController.GetCommand(Settings.Bot.CommandNames.AddMerch),
            CommandController.GetCommand(Settings.Bot.CommandNames.DeleteMerch)

        };
        protected static Command[] MenuCommands = new Command[] {
            CommandController.GetCommand (Settings.Bot.CommandNames.MyAccount),
            CommandController.GetCommand (Settings.Bot.CommandNames.Settings),
            CommandController.GetCommand (Settings.Bot.CommandNames.Events),
            CommandController.GetCommand (Settings.Bot.CommandNames.DailyGame)
        };
    }
}