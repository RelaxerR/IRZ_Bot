/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Контроллер цвета сообщения
    /// Опрделяет свой цвет для каждого ноого класса
    /// </summary>
    static class LogColorController
    {
        public static List<string> LogGroups = new List<string>();
        public static List<ConsoleColor> LogColors = new List<ConsoleColor>();

        /// <summary>
        /// Получает цвет для рзных классов
        /// </summary>
        /// <param name="groupName">Название класса</param>
        /// <returns>Цвет класса</returns>
        public static ConsoleColor GetColor (string groupName) {
            var id = LogGroups.FindIndex (i => i == groupName);
            if (id < 0)
            {
                var newColor = CreateNewColor (groupName);
                return newColor;
            }
            return LogColors[id];
        }

        /// <summary>
        /// Содает случайный цвет для нового класса
        /// </summary>
        /// <param name="groupName">Название класса</param>
        /// <returns>Цвет класса</returns>
        private static ConsoleColor CreateNewColor (string groupName) {
            LogGroups.Add (groupName);

            ConsoleColor[] consoleColors =
                { ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkMagenta, ConsoleColor.Magenta, };
            var newColor = consoleColors[Settings.rnd.Next (consoleColors.Length)];

            LogColors.Add (newColor);
            return newColor;
        }
    }
}