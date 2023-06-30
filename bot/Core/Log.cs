/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System;

namespace Core
{
    /// <summary>
    /// Использовать вместо Console
    /// За основу взят класс Unity.
    /// Взаимодейтсвует с LogColorController
    /// </summary>
    static class Debug
    {
        private static string text;
        private static string initTime;
        private static object prevMsgParent;
        
        /// <summary>
        /// Запускать в самом начале программы
        /// </summary>
        /// <param name="_allowLogging">Параметр устанавливается в Settings</param>
        public static void Init (bool _allowLogging = true) {
            initTime = DateTime.Now.ToLongTimeString ().Replace (':', '+');
            Settings.Debug.InitLogging (_allowLogging);
        }

        /// <summary>
        /// Обычное сообщение
        /// </summary>
        /// <param name="line">Текст сообщения</param>
        /// <param name="className">Название класса, из которого вызываеся (this)</param>
        public static void Log (string line, object className = null) {
            if (className == null) className = "summary";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = LogColorController.GetColor (className.ToString ()); 

            var addLine = $"{line}";
            Print (addLine, className);
        }

        /// <summary>
        /// Предупредение (пишется желтым цветом)
        /// </summary>
        /// <param name="line">Текст сообшеия</param>
        /// <param name="className">Название класса, из которого вызываеся (this)</param>
        public static void LogWarning (string line, object className = null) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            var addLine = $"[Warning] {line}";
            Print (addLine, className);
        }

        /// <summary>
        /// Сообщение об ошибке (выделяется красным)
        /// </summary>
        /// <param name="line">Текст сообщения</param>
        /// <param name="className">Название класса, из которого вызываеся (this)</param>
        public static void LogError (string line, object className = null) {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            var addLine = $"[Error] {line}";
            Print (addLine, className);
        }

        /// <summary>
        /// Сообщение о критической ошибке и вероятной остановке выполнения программы (выделяется белым текстом на красном фоне)
        /// </summary>
        /// <param name="line">Текст ошибки</param>
        /// <param name="className">Название класса, из которого вызываеся (this</param>
        public static void LogCriticalError (string line, object className = null) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            var addLine = $"[---CRITICAL ERROR---]\n{line}";
            Print (addLine, className);

            _ = TeleBot.BotController.SendMessageToAdmins (addLine);
        }

        /// <summary>
        /// Вывод в консоль со временем и классом
        /// Сообщение от одного и того же класса выделяются одним цветом.
        /// Несолько сообщений от одного класса идущие подряд, отдеяются пробелами
        /// </summary>
        /// <param name="addLine">Текст сообщения</param>
        /// <param name="className">Название класса</param>
        private static void Print (string addLine, object className = null) {
            if (!Settings.Debug.AllowLogging) return;
            if (className == null) className = "summary";

            if (prevMsgParent != className)
                addLine = $"\n[{className}] [{DateTime.Now}]: {addLine}";
            else
                addLine = $"[{className}] [{DateTime.Now}]: {addLine}";
            text += $"{addLine}\n";
            prevMsgParent = className;

            Console.WriteLine (addLine);
            Save ();
        }

        private static void Save () => Data.SaveLog (text, initTime);
    }
}