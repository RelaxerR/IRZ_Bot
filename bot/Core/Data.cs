/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System;
using System.IO;
using TeleBot;

namespace Core
{
    /// <summary>
    /// Взаимодейстиве с данными
    /// </summary>
    static class Data
    {
        /// <summary>
        /// Вызывать в начале выполнения программы
        /// </summary>
        public static void Init () {
            CreatePath (Settings.Data.Path_Log);
            CreatePath (Settings.Data.Path_Resources);
            CreatePath (Settings.Data.Path_Data);
        }

        /// <summary>
        /// Содать путь
        /// </summary>
        /// <param name="path">Путь</param>
        private static void CreatePath (string path) {
            Debug.Log ($"Creating path: {path}", "Data");
            if (Directory.Exists (path))
            {
                Debug.Log ($"Path already exist", "Data");
                return;
            }
            Directory.CreateDirectory (path);
            Debug.Log ($"Path created!", "Data");
        }

        #region Log
        public static void SaveLog (string logText, string logInitTime) {
            try
            {
                File.WriteAllText ($@"{Settings.Data.Path_Log}/started_at_{logInitTime}.txt", logText);
            }
            catch (Exception ex)
            {
                Console.WriteLine ($"[Log file NOT saved.\nError message: {ex.Message}");
            }
        }
        #endregion
        #region Data
        public static void GetAllUsersIds()
        {
            Debug.Log("Trying to get user id list...", "Data");
            try
            {
                var files = Directory.GetFiles($"{Settings.Data.Path_Data}");
                foreach (string file in files)
                {
                    var fname = Path.GetFileName(file).Replace(".txt", string.Empty);
                    BotUserController.GetUser(int.Parse(fname));
                }
                Debug.LogError("Success", "Data");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error\n{ex.Message}", "Data");
            }
        }

        /// <summary>
        /// Сохранить данные профиля пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        public static void SaveUser(BotUser user)
        {
            Debug.Log($"Saving user id {user.MyUser.Id} data...", "Data");
            File.WriteAllLines($"{Settings.Data.Path_Data}/{user.MyUser.Id}.txt", user.Data);
            Debug.Log($"Success!", "Data");
        }

        /// <summary>
        /// Загрузить данные профиля пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns>Массив с данными профиля</returns>
        public static bool LoadUser(BotUser user)
        {
            Debug.Log($"Loading user id {user.MyUser.Id} data...", "Data");
            if (!File.Exists($"{Settings.Data.Path_Data}/{user.MyUser.Id}.txt"))
            {
                Debug.LogWarning($"File not exist!", "Data");
                return false;
            }
            var data = File.ReadAllLines($"{Settings.Data.Path_Data}/{user.MyUser.Id}.txt");
            user.AddData(data);

            try
            {
                user.MyPoints = int.Parse(data[6]);
                Debug.Log($"User points loaded!", "Data");
            }
            catch
            {
                Debug.LogWarning($"User points not loaded! Incorrect data format", "Data");
            }
            Debug.Log($"Success!", "Data");
            return true;
        }

        /// <summary>
        /// Удаляет файл с данными пользователя
        /// </summary>
        /// <param name="userId">Telegram ID пользователя</param>
        /// <returns></returns>
        public static bool DeleteUser(string userId)
        {
            Debug.LogWarning($"Deleting user id {userId} data...", "Data");
            if (!File.Exists($"{Settings.Data.Path_Data}/{userId}.txt"))
            {
                Debug.LogWarning($"File not exist!", "Data");
                return false;
            }
            File.Delete($"{Settings.Data.Path_Data}/{userId}.txt");
            Debug.Log($"Success!", "Data");
            return true;
        }

        /// <summary>
        /// Сохранить мерч
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="cost">Стоимость</param>
        public static void SaveMerch(string name, int cost)
        {
            Debug.Log($"Trying to save merch {name} - {cost}...", "Data");
            try
            {
                File.WriteAllText($"{Settings.Data.Path_Resources}/{name}.txt", cost.ToString());
                Debug.Log($"Success!", "Data");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Not saved\n{ex.Message}", "Data");
            }
        }

        /// <summary>
        /// Получить стоимоть мерча
        /// </summary>
        /// <param name="name">Название мерча</param>
        /// <returns>Стоимость мерча</returns>
        public static int GetMerchCost(string name)
        {
            Debug.Log($"Trying to get merch cost {name}...", "Data");
            try
            {
                var data = int.Parse(File.ReadAllText($"{Settings.Data.Path_Resources}/{name}.txt"));
                Debug.Log($"Success! cost: {data}", "Data");
                return (data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Not got\n{ex.Message}", "Data");
                return int.MaxValue;
            }
        }

        /// <summary>
        /// Удалиь мерч
        /// </summary>
        /// <param name="name">Название мерча</param>
        /// <returns>Стоимость мерча</returns>
        public static bool DeleteMerch(string name)
        {
            Debug.Log($"Trying to delete merch {name}...", "Data");
            try
            {
                File.Delete($"{Settings.Data.Path_Resources}/{name}.txt");
                Debug.Log($"Success!", "Data");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Not deleted\n{ex.Message}", "Data");
                return false;
            }
        }

        /// <summary>
        /// Получить список названий мерча
        /// </summary>
        /// <returns>Список названий ерча</returns>
        public static string[] GetMerchList()
        {
            Debug.Log($"Trying to get merch list...", "Data");
            try
            {
                var files = Directory.GetFiles($"{Settings.Data.Path_Resources}");
                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = Path.GetFileName(files[i]).Replace(".txt", string.Empty);
                }
                Debug.Log($"Success! arr length: {files.Length}", "Data");
                return files;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Not got\n{ex.Message}", "Data");
                return new string[1];
            }
        }
        #endregion
        #region Bot
        #endregion
    }
}