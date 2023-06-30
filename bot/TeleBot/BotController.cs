/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using Core;

namespace TeleBot
{
    /// <summary>
    /// Управляет ботом
    /// </summary>
    static class BotController
    {
        /// <summary>
        /// Все возможные состояния пользователя (для ввода данных)
        /// </summary>
        public enum State
        {
            Default,
            AddPoints,
            DeletePoints,
            WriteMessage,
            FIO,
            Phone,
            Department,
            Post,
            Ban,
            AddMerch,
            DeleteMerch,
            ChooseMerch
        }

        private static TelegramBotClient botClient;

        /// <summary>
        /// Вызывать в начале кода
        /// </summary>
        public static void Init () {
            Debug.Log ("Initializing bot...", "Bot");
            botClient = new TelegramBotClient (Settings.Bot.Api_key);
            botClient.StartReceiving (MessageController.Update, MessageController.Error);
            //botClient.Timeout = new TimeSpan (0, 0, 5);

            Debug.Log ("Success!", "Bot");
        }

        /// <summary>
        /// Отправляет сообщение с клавиатурой
        /// </summary>
        /// <param name="userId">Telegam ID пользователя</param>
        /// <param name="text">Текст соощения</param>
        /// <param name="keyboard">Клавиатура (для оздания клавиатуры из массива (или массива команд) - GetKeyboardFromArray)</param>
        /// <returns>Экземпляр отправленного сообщения</returns>
        public static async Task SendMessage (long userId, string text, ReplyKeyboardMarkup keyboard) {
            Debug.Log ($"Sending message to user id: {userId} (with keyboard)\nText: {text}...", "Bot");
            await botClient.SendTextMessageAsync (userId,
                text,
                parseMode: ParseMode.Html,
                replyMarkup: keyboard);
            Debug.Log ($"Success!", "Bot");
        }

        /// <summary>
        /// Оправляет сообщение без клавиатуры
        /// </summary>
        /// <param name="userId">Telegram ID пользователя</param>
        /// <param name="text">Текст сообщения</param>
        /// <returns>Экземпляр отправленного сообщения</returns>
        public static async Task SendMessage (long userId, string text) {
            Debug.Log ($"Sending message to user id: {userId} (without keyboard)\nText: {text}...", "Bot");
            await botClient.SendTextMessageAsync (userId,
                text,
                parseMode: ParseMode.Html);
            Debug.Log ($"Success!", "Bot");
        }

        /// <summary>
        /// Отправляет сообщение всем Администраторам
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        /// <returns>Экземпляр отправленного сообщения</returns>
        public static async Task SendMessageToAdmins (string text) {
            Debug.LogWarning ($"Sending message to ADMINS!\nText: {text}...", "Bot");
            foreach (long id in Settings.Bot.AdminsId)
            {
                await SendMessage (id, text);
                Debug.LogWarning ($"Sended to {id}!", "Bot");
            }
            Debug.LogWarning ($"Success!", "Bot");
        }

        /// <summary>
        /// Содает клавиатуру из массива команд
        /// </summary>
        /// <param name="arr">Массив команд</param>
        /// <returns>Экземпляр клавиатуры</returns>
        public static ReplyKeyboardMarkup GetKeyboardFromArray (Command[] arr) {
            Debug.Log ($"Generating keyboard from command[]...", "Bot");
            foreach (Command cmd in arr)
            {
                if (cmd == null)
                {
                    Debug.LogError ($"Command is null!", "Bot");
                }
                Debug.Log ($"found cmd: {cmd.Name}", "Bot");
            }
            var rkm = new ReplyKeyboardMarkup ("");
            var rows = new List<KeyboardButton[]> ();
            var cols = new List<KeyboardButton> ();

            for (var Index = 0; Index < arr.Length; Index++)
            {
                cols.Add (new KeyboardButton ("" + arr[Index].Name));
                if ((Index + 1) % 3 == 0 || Index + 1 == arr.Length)
                {
                    rows.Add (cols.ToArray ());
                    cols = new List<KeyboardButton> ();
                }
            }
            rkm.Keyboard = rows.ToArray ();
            rkm.ResizeKeyboard = true;
            Debug.Log($"Success!", "Bot");
            return rkm;
        }

        /// <summary>
        /// Создает клавиатуру из массива строк
        /// </summary>
        /// <param name="arr">Массив строк</param>
        /// <returns>Экземпляр клавиатуры</returns>
        public static ReplyKeyboardMarkup GetKeyboardFromArray (string[] arr) {
            Debug.Log($"Generating keyboard from string[]...", "Bot");
            var rkm = new ReplyKeyboardMarkup ("");
            var rows = new List<KeyboardButton[]> ();
            var cols = new List<KeyboardButton> ();

            for (var Index = 0; Index < arr.Length; Index++)
            {
                cols.Add (new KeyboardButton ("" + arr[Index]));
                if ((Index + 1) % 3 == 0 || Index + 1 == arr.Length)
                {
                    rows.Add (cols.ToArray ());
                    cols = new List<KeyboardButton> ();
                }
            }
            rkm.Keyboard = rows.ToArray ();
            rkm.ResizeKeyboard = true;
            Debug.Log($"Success!", "Bot");
            return rkm;
        }

        public static void RestartBot()
        {
            Debug.LogError($"Trying to restart bot...", "Bot");

            Debug.Init();
            Data.Init();
            BotController.Init();
            CommandController.Init();

            SendMessageToAdmins($"[Bot started]");
        }
    }
}