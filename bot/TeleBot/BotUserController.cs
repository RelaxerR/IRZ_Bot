/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System.Collections.Generic;
using Telegram.Bot.Types;
using Core;

namespace TeleBot
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    static class BotUserController
    {
        public static List<BotUser> TrenerUsers = new List<BotUser>();

        /// <summary>
        /// Находит пользователя по Telegram Ползователю (если не существует, создает нового)
        /// </summary>
        /// <param name="user">ользователь Телеграма</param>
        /// <returns>Модель пользователя</returns>
        public static BotUser GetUser (User user) {
            Debug.Log ($"Trying to get user id: {user.Id} (name: {user.FirstName})", "TrenerUserController");
            var botUser = TrenerUsers.Find (x => x.MyUser.Id == user.Id);
            if (botUser == null)
            {
                botUser = new BotUser (user);
                TrenerUsers.Add (botUser);
                Debug.Log ($"Created new trener user id: {user.Id} (name: {user.FirstName})", "TrenerUserController");
            }
            Debug.Log ($"Success!", "TrenerUserController");
            return botUser;
        }

        /// <summary>
        /// Находит пользователя по Telegram ID (если не существует, создает нового)
        /// </summary>
        /// <param name="userId">Telegram ID</param>
        /// <returns>Модель пользователя</returns>
        public static BotUser GetUser(long userId)
        {
            Debug.Log($"Trying to get user by id: {userId}", "TrenerUserController");
            var botUser = TrenerUsers.Find(x => x.MyUser.Id == userId);
            if (botUser == null)
            {
                User user = new User()
                {
                    Id = userId
                };

                botUser = new BotUser(user);
                TrenerUsers.Add(botUser);
                Debug.Log($"Created new trener user id: {user.Id} (name: {user.FirstName})", "TrenerUserController");
            }
            else
            {
                Debug.Log($"Success!", "TrenerUserController");
            }
            return botUser;
        }

    }
}