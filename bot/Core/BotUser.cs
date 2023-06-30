/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Core
{
    /// <summary>
    /// Модель пользователя Бота.
    /// </summary>
    class BotUser
    {
        /// <summary>
        /// Экземпляр польователя Телеграмм
        /// </summary>
        public User MyUser { get; set; }
        /// <summary>
        /// Текущие доступные пользователю команды
        /// </summary>
        public List<Command> AllowedCommands { get; set; }
        /// <summary>
        /// Текущее состояние польователя
        /// </summary>
        public TeleBot.BotController.State MyState { get; set; }
        /// <summary>
        /// Баллы пользователя
        /// </summary>
        public int MyPoints { get; set; }
        /// <summary>
        /// Анкета (Профиль) пользователя
        /// </summary>
        public string[] Data { get; set; } = new string[Settings.Data.UserDataLength];
        /// <summary>
        /// ПОдписан ли пользователь на рассылку
        /// </summary>
        public bool IsSubscribed { get; set; }
        public bool tmp { get; set; }

        public BotUser (User user) {
            MyUser = user;
            MyState = TeleBot.BotController.State.Default;
            IsSubscribed = false;
            Core.Data.LoadUser(this);
            AllowedCommands = new List<Command> ()
            {
                TeleBot.CommandController.GetCommand (Settings.Bot.CommandNames.Start)
            };
        }

        /// <summary>
        /// Добавляет баллы пользователю
        /// </summary>
        /// <param name="count">Кол-во баллов</param>
        /// <param name="reason">Причина добавления баллов (если не указана, добавляется по умолчанию)</param>
        public async void AddPoints (int count, string reason = "")
        {
            MyPoints += count;
            if (reason == "")
                reason = Settings.Bot.Messages.YouGetPointsDefaultArg;

            await TeleBot.BotController.SendMessage(MyUser.Id,
                $"{Settings.Bot.Messages.YouGetPoints(count, this)} {reason}");
            AddData(6, MyPoints.ToString());
            Debug.Log($"Added {count} points to user id {MyUser.Id} ({MyUser.FirstName})!", this);
        }

        /// <summary>
        /// Вычитает баллы пользователя
        /// </summary>
        /// <param name="count">Кол-во баллов</param>
        /// <param name="reason">Причина вычитания баллов (если не указана, обавляется по умолчанию)</param>
        public async void DeletePoints(int count, string reason = "")
        {
            MyPoints -= count;
            if (reason == "")
                reason = Settings.Bot.Messages.YouLostPointsDefaultArg;

            await TeleBot.BotController.SendMessage(MyUser.Id,
                $"{Settings.Bot.Messages.YouLostPoints(count)} {reason}");
            AddData(6, MyPoints.ToString());
            Debug.Log($"Deleted {count} points from user id {MyUser.Id} ({MyUser.FirstName})!", this);
        }

        /// <summary>
        /// Добавляет данные в анкету пользователя
        /// </summary>
        /// <param name="id">Номер элемента в массиве (0 - Telegram id. 1 - имя пользователя. 2 - ФИО. 3 - телефон. 4 - отдел. 5 - должноть. 6 - кол-во баллов)</param>
        /// <param name="value">Значение</param>
        public void AddData(int id, string value)
        {
            Data[id] = value;
            Debug.Log($"Added data to user id {MyUser.Id}: [{id}]: {value}", this);
            Core.Data.SaveUser(this);
        }
        /// <summary>
        /// Устанавливает все данные пользователя
        /// </summary>
        /// <param name="newData">Новые данные (При изменении структуры данных, изменить параметр в Settings)</param>
        public void AddData(string[] newData)
        {
            Data = newData;
            Debug.Log($"Updated data to user id {MyUser.Id}", this);
            Core.Data.SaveUser(this);
        }

        public bool IsRegistred() => Core.Data.LoadUser(this);

        /// <summary>
        /// Получает данные профиля пользователя
        /// </summary>
        /// <returns>Строка со всеми данными</returns>
        public string GetData()
        {
            var txt = "";
            try
            {
                txt = $"ID: @{Data[0]}\n" +
                    $"Username: @{Data[1]}\n" +
                    $"ФИО: {Data[2]}\n" +
                    $"Телефон: {Data[3]}\n" +
                    $"Отдел: {Data[4]}\n" +
                    $"Должность: {Data[5]}\n";
            }
            catch
            {
                foreach (string d in Data)
                {
                    txt += $"{d}\n";
                }
            }
            return txt;
        }

        public void Subscribe()
        {
            if (!IsSubscribed) IsSubscribed = true;
        }
        public void Unsubscribe()
        {
            if (!IsSubscribed) IsSubscribed = false;
        }
    }
}