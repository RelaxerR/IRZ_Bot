/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System;
using System.Drawing;


namespace Core
{
    /// <summary>
    /// Все настройки программы
    /// </summary>
    static class Settings
    {
        /// <summary>
        /// Для случайных чисел использовть это
        /// </summary>
        public static Random rnd { get; set; } = new Random();

        /// <summary>
        /// Настройки бота
        /// </summary>
        public static class Bot
        {

            public const string Api_key = "6000195352:AAFbUg40kZ-QWpPqLbVIUWkyFftCKnfs-XY";
            public static readonly long[] AdminsId = { 1105052146 }; //1105052146, 1181453430
            public const int MaxButtonsCount = 4; // Максимальное число кнопок для одной линии
                        //using (var fileStream = new FileStream(mypath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        //{
                        //    await _botService.Client.SendPhotoAsync(
                        //        chatId: message.Chat.Id,
                        //        photo: new InputOnlineFile(fileStream),
                        //        caption: "My Photo"
                        //    );
                        //}
/// <summary>
/// Все сообщения бота
/// </summary>
public static class Messages
            {
                public static string Hello => "Привет!&#128075; Меня зовут Радио-Н, и я стану Вашим проводником в корпоративную жизнь 'ИРЗ'&#128522;";
                public static string MyPoints(int pointsCount) => $"Ваши звездочки: {pointsCount}";
                public static string AddPoints => $"Введите id пользователя, сколько звездочек добавить и причину добавления (например 1105052146_50_за хорошее настроение)";
                public static string AddPointsError => $"Неверный формат данных! Введите id пользователя и сколько звездочек добавить (например 1105052146_50_за хорошее настроение)";
                public static string DeletePoints => $"Введите id пользователя и сколько звездочек снять (например 1105052146_50_за плохое настроение)";
                public static string DeletePointsError => $"Неверный формат данных! Введите id пользователя, сколько звездочек снять и причину снятия (например 1105052146_50_за плохое настроение)";
                public static string UserNotFound => $"Пользователь не найден";
                public static string WriteMessage => $"Напишите сообщение для рассылки\n(Если вы хотите отправить рассылку только конкретному отделу, напишите сначала номер отдела, а затем через _ текст)";
                public static string DefaultMessage => $"Сообщение рассылки:\n";
                public static string MailsSended => $"Отправлено сообщение рассылки";
                public static string YouGetPoints (int pointsCount, BotUser user) => $"Вы получили {pointsCount}{PointsName}! (Ваш баланс: {user.MyPoints}{PointsName})";
                public static string YouLostPoints (int pointsCount) => $"Вы потеряли ({pointsCount}) {PointsName}";
                public static string UserGotPoints (int pointsCount, string userName, string reason) => $"Пользователь {userName} получил ({pointsCount}) {PointsName} {reason}";
                public static string UserLostPoints (int pointsCount, string userName, string reason) => $"Пользователь {userName} потерял ({pointsCount}) {PointsName} {reason}";
                public static string YouGetPointsDefaultArg => $"за хорошее настроение :)";
                public static string YouLostPointsDefaultArg => $"за плохое настроение :(";
                public static string WorkInProgress => $"Данная функция находится в разработке. Приносим свои извинения";
                // !!! убрать-убрать-убрать !!!
                public static string Kostil => $"4 июля - мастер-класс 'Эмоциональный интеллект'\n7 июля - соревнования по легкой атлетике\n"; //большой-большой костыль
                public static string YouInAccount => $"В данном меню Вы можете посмотреть информацию о Вашем аккаунте";
                public static string YouInMenu => $"Вы в Главном Меню";
                public static string YouInSettings => $"Вы в Настройках";
                public static string WriteFIO => $"Напишите Ваше ФИО";
                public static string WritePhone => $"Напишите Ваш номер телефона (начинается на +7)";
                public static string WriteDepartment => $"Напишите номер Вашего отдела";
                public static string WritePost => $"Напишите Вашу должность";
                public static string YouRegistred => $"Спасибо за регистрацию!";
                public static string UserRegistred (BotUser u) => $"Зарегистрирован новый пользователь:\n{u.GetData()}";
                public static string WriteUserId => $"Напишите ID пользователя, чтобы удалить его данные. Внимание! Это действие невозможно отменить!";
                public static string YouBanned => $"Ваш аккаунт удален! Пожалуйста, зарегистрируйтесь заново";
                public static string UserBanned => $"Данные пользователя удалены";
                public static string UserNotBanned => $"Не удалось найти даннные пользователя. Попробуйте еще раз";
                public static string YouSubscribed => $"Вы подписались на рассылку!";
                public static string YouUnsubscribed => $"Вы отписались от рассылки!";
                public static string UserUnsubscribed(BotUser user) => $"Пользователь @{user.MyUser.Username} (id: @{user.MyUser.Id}) отписался от рассылки!";
                public static string UserSubscribed(BotUser user) => $"Пользователь @{user.MyUser.Username} (id: @{user.MyUser.Id}) подписался на рассылку!";
                public static string MyData(BotUser user) => $"Пользователь @{user.MyUser.Username} (id: @{user.MyUser.Id}) подписался на рассылку!";
                public static string UnknownCommand => $"Неизвестная команда!";
                public static string AddMerch => $"Укажите название и стоимость мерча через _";
                public static string DeleteMerch => $"Укажите название мерча для удаления";
                public static string ChooceMerch => $"Выберите, что хотие купить из списка";
                public static string PointsName => $"&#127775;";
                public static string MerchListName => $"Список мерча (введите номер мерча, чтобы купить его):";
                public static string MerchAdded => $"Мерч добавлен";
                public static string MerchNotAdded => $"Неверный формат данных";
                public static string MerchDeleted => $"Мерч удален";
                public static string MerchNotDeleted => $"Мерч не найден";
                public static string YouBuyMerch (string name, int cost) => $"Вы купили {name} за {cost}{PointsName}";
                public static string YouNotBuyMerch => $"Мерч не найден";
                public static string YouHaventPoints (int cost, int UserPoints) => $"У Вас не хватает {cost - UserPoints}{PointsName}";
                public static string UserBuyMerch (BotUser u, string name) => $"Пользователь @{u.MyUser.Username} купил {name}";
            }

            /// <summary>
            /// Все названия команд бота
            /// </summary>
            public static class CommandNames
            {
                public static string Start { get; } = "/start";
                public static string Profile { get; } = "Мой профиль";
                public static string Event { get; } = "Ближайшие мероприятия";
                public static string GameDay { get; } = "Игра дня";
                public static string GetPoints { get; } = "Показать звездочки";
                public static string AddPoints { get; } = "Добавить звездочки";
                public static string ShowUsers { get; } = "Показать пользователей";
                public static string DeletePoints { get; } = "Снять звездочки";
                public static string Mail { get; } = "Рассылка";
                public static string MyAccount { get; } = "Мой профиль";
                public static string Settings { get; } = "Настройки";
                public static string Events { get; } = "Ближайшие события";
                public static string DailyGame { get; } = "Игра дня";
                public static string Menu { get; } = "Меню";
                public static string Register { get; } = "Регистрация";
                public static string Ban { get; } = "Блокировка аккаунта";
                public static string Subscribe { get; } = "Подписаться";
                public static string Unsubscribe { get; } = "Отписаться";
                public static string ShowData { get; } = "Информаиция об аккаунте";
                public static string BuyMerch { get; } = "Обменять звездочки на мерч";
                public static string AddMerch { get; } = "Добавить мерч";
                public static string DeleteMerch { get; } = "Удалить мерч";

            }
        }

        /// <summary>
        /// Настройки данных
        /// </summary>
        public static class Data
        {
            public const string Path_Log = "Logs";
            public const string Path_Resources = "Resources";
            public const string Path_Data = "Data";

            public const string EXECUTE_PATH = @"C:\Users\user\source\repos\bot\bot\bin\Release\netcoreapp3.1\bot.exe";
            /// <summary>
            /// Длина массива данных пользователя
            /// </summary>
            public static int UserDataLength { get; set; } = 7;
        }

        /// <summary>
        /// Настройки логирования
        /// </summary>
        public static class Debug
        {
            public static bool AllowLogging { get; private set; }
            public static void InitLogging (bool val) => AllowLogging = val;
            public static void ChangeLogging () => AllowLogging = !AllowLogging;
        }

        public static class Achivements
        {
            public static string Register => $"Ура! Достижение выполнено: Вы зарегистрировались!";
            public static string Buy => $"Ура! Достижение выполнено: Вы купили мерч!";
            public static string Subscribe => $"Ура! Достижение выполнено: Вы подписались на рассылку!";
        }

        /// <summary>
        /// Именяет строку, делая певый имвол заглавным
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns>Возвращает строку, в которой первый символ заглавный</returns>
        public static string Capitalize(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return s;
            }
            return s[0].ToString().ToUpper() + s.Substring(1);
        }
    }
}