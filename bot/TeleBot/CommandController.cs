/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System.Collections.Generic;
using Commands;
using Core;

namespace TeleBot
{
    /// <summary>
    /// Контроллер команд
    /// </summary>
    static class CommandController
    {
        public static List<Core.Command> BotCommands = new List<Core.Command> ();

        /// <summary>
        /// Вызывать в начале кода.
        /// Сюда прописывать все новые команды (добавлять их в список)
        /// </summary>
        public static void Init()
        {
            AddCommand(new CommandStart());
            AddCommand(new CommandGetPoins());
            AddCommand(new CommandAddPoints());
            AddCommand(new CommandDeletePoints());
            AddCommand(new CommandShowUsers());
            AddCommand(new CommandMail());
            AddCommand(new CommandMyAccount());
            AddCommand(new CommandSettings());
            AddCommand(new CommandEvents());
            AddCommand(new CommandDailyGame());
            AddCommand(new CommandMenu());
            AddCommand(new CommandRegister());
            AddCommand(new CommandBan());
            AddCommand(new CommandSubscribe());
            AddCommand(new CommandUnsubscribed());
            AddCommand(new CommandShowData());
            AddCommand(new CommandAddMerch());
            AddCommand(new CommandDeleteMerch());
            AddCommand(new CommandBuyMerch());
        }
        public static void AddCommand (Command command) {
            BotCommands.Add (command);
        }

        /// <summary>
        /// Получает коману по ее названию
        /// </summary>
        /// <param name="name">Название команды (из Settings.Bot.CommandNames)</param>
        /// <returns>Экземпляр команды</returns>
        public static Command GetCommand (string name) {
            Debug.Log ($"Trying to find command: {name}\nCommands length: {BotCommands.Count}", "CommandController");
            return BotCommands.Find (x => x.Name == name);
        }
    }
}