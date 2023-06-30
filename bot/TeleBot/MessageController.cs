/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Collections.Generic;
using Core;

namespace TeleBot
{
    /// <summary>
    /// Контроллер сообщений
    /// </summary>
    static class MessageController
    {
        /// <summary>
        /// Основная точка получению новых сообщений
        /// </summary>
        /// <param name="client"></param>
        /// <param name="update">Данные о сообщении и пользователе</param>
        /// <param name="token"></param>
        /// <returns></returns>
        #pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        public async static Task Update (ITelegramBotClient client, Update update, CancellationToken token) {
            var message = update.Message;
            if (message == null)
                return;

            var chatId = message.Chat.Id;
            var firstName = message.Chat.FirstName;
            var userName = message.Chat.Username;
            var text = message.Text;
            var user = message.From;

            Debug.Log($"Chat id: {chatId} | First name: {firstName} | User name: {userName}\nText: {text}", "New_Message");
            var botUser = BotUserController.GetUser(message.From);
            if (botUser.MyUser.Username == "" || botUser.MyUser.Username == null)
                botUser.MyUser = user;

            var executed = false;
            Debug.Log("Finding command...", "MessageController");
            foreach (Command cmd in botUser.AllowedCommands)
            {
                
                if (cmd.Name == text)
                {
                    Debug.Log($"Found command {text}", "MessageController");
                    cmd.Execute(message.From);
                    executed = true;
                    break;
                }
            }
            if (!executed)
            {
                switch (botUser.MyState)
                {
                    case BotController.State.AddPoints:
                        try
                        {
                            var data = text.Split('_');
                            var userId = data[0];
                            var pointsCount = data[1];
                            var arg = Settings.Bot.Messages.YouGetPointsDefaultArg;
                            if (data.Length > 2)
                                arg = data[2];

                            var targeBotUser = BotUserController.GetUser(Convert.ToInt64 (userId));
                            if (targeBotUser == null)
                            {
                                await BotController.SendMessage(user.Id, Settings.Bot.Messages.UserNotFound);
                                break;
                            }
                            targeBotUser.AddPoints(int.Parse(pointsCount), arg);
                            botUser.MyState = BotController.State.Default;
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.UserGotPoints(int.Parse(pointsCount), userId, arg));
                        }
                        catch (Exception ex)
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.AddPointsError + $"\n{ex.Message}");
                        }
                        break;
                    case BotController.State.DeletePoints:
                        try
                        {
                            var data = text.Split('_');
                            var userId = data[0];
                            var pointsCount = data[1];
                            var arg = Settings.Bot.Messages.YouLostPointsDefaultArg;
                            if (data.Length > 2)
                                arg = data[2];

                            var targeBotUser = BotUserController.GetUser(Convert.ToInt64(userId));
                            if (targeBotUser == null)
                            {
                                await BotController.SendMessage(user.Id, Settings.Bot.Messages.UserNotFound);
                                break;
                            }
                            targeBotUser.DeletePoints(int.Parse(pointsCount), arg);
                            botUser.MyState = BotController.State.Default;
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.UserLostPoints(int.Parse(pointsCount), userId, arg));
                        }
                        catch (Exception ex)
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.DeletePointsError + $"\n{ex.Message}");
                        }
                        break;
                    case BotController.State.WriteMessage:
                        var args = text.Split('_');
                        var departmet = "";
                        var mailText = "";
                        if (args.Length > 1)
                        {
                            departmet = args[0];
                            mailText = args[1];
                        }
                        else
                        {
                            mailText = text;
                        }
                        foreach (BotUser u in BotUserController.TrenerUsers)
                        {
                            if (u.IsSubscribed)
                                if (departmet != "")
                                {
                                    if (u.Data[3] == departmet)
                                        await BotController.SendMessage(u.MyUser.Id, $"{Settings.Bot.Messages.DefaultMessage}\n{text}");
                                }
                                else
                                {
                                    await BotController.SendMessage(u.MyUser.Id, $"{Settings.Bot.Messages.DefaultMessage}\n{text}");
                                }
                        }
                        await BotController.SendMessageToAdmins($"{Settings.Bot.Messages.MailsSended}:\n{text}");
                        break;
                    case BotController.State.FIO:
                        if (text.Split(' ').Length == 3)
                        {
                            botUser.AddData(2, text);
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.WritePhone);
                            botUser.MyState = BotController.State.Phone;
                        }
                        else
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.WriteFIO);
                        }
                        break;
                    case BotController.State.Phone:
                        if (text.StartsWith('+') && text.Length == 12)
                        {
                            botUser.AddData(3, text);
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.WriteDepartment);
                            botUser.MyState = BotController.State.Department;
                        }
                        else
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.WritePhone);
                        }
                        break;
                    case BotController.State.Department:
                        try
                        {
                            int.Parse(text); // :\

                            botUser.AddData(4, Settings.Capitalize(text.Trim()));
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.WritePost);
                            botUser.MyState = BotController.State.Post;
                        }
                        catch
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.WriteDepartment);
                        }
                        break;
                    case BotController.State.Post:
                        botUser.AddData(5, Settings.Capitalize(text.Trim()));
                        await BotController.SendMessage(user.Id, Settings.Bot.Messages.YouRegistred);
                        botUser.MyState = BotController.State.Default;
                        //CommandController.GetCommand(Settings.Bot.CommandNames.Subscribe).Execute(user);
                        CommandController.GetCommand(Settings.Bot.CommandNames.Menu).Execute(user);
                        await BotController.SendMessageToAdmins(Settings.Bot.Messages.UserRegistred(botUser));

                        botUser.AddPoints(50, Settings.Achivements.Register);
                        break;
                    case BotController.State.Ban:
                        if (Data.DeleteUser(text))
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.UserBanned);
                            try
                            {
                                var targetUser = BotUserController.GetUser(Convert.ToInt64(text));
                                targetUser.AllowedCommands = new List<Command>();
                                targetUser.AllowedCommands.Add(CommandController.GetCommand(Settings.Bot.CommandNames.Register));
                                await BotController.SendMessage(targetUser.MyUser.Id, Settings.Bot.Messages.YouBanned, BotController.GetKeyboardFromArray(targetUser.AllowedCommands.ToArray()));
                            }
                            catch
                            {
                                Debug.LogError($"Cannot convert user ban id to long type. MEssage to target user no sended!", "MessageController");
                            }
                        }
                        else
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.UserNotBanned);
                        }
                        botUser.MyState = BotController.State.Default;
                        break;
                    case BotController.State.AddMerch:
                        try
                        {
                            var data = text.Split('_');
                            var name = data[0];
                            var cost = int.Parse(data[1]);

                            Data.SaveMerch(name, cost);
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.MerchAdded);
                        }
                        catch
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.MerchNotAdded);
                        }
                        break;
                    case BotController.State.DeleteMerch:
                        try
                        {
                            if (!Data.DeleteMerch(text))
                                throw new Exception("Not deleted!");
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.MerchDeleted);
                        }
                        catch
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.MerchNotDeleted);
                        }
                        break;
                    case BotController.State.ChooseMerch:
                        var merchList = Data.GetMerchList();
                        try
                        {
                            var id = int.Parse(text);
                            var merch = merchList[id];
                            var cost = Data.GetMerchCost(merch);

                            if (cost > botUser.MyPoints)
                            {
                                await BotController.SendMessage(user.Id, Settings.Bot.Messages.YouHaventPoints(cost, botUser.MyPoints));
                                break;
                            }

                            botUser.DeletePoints(cost, merch);
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.YouBuyMerch (merch, cost));
                            await BotController.SendMessageToAdmins(Settings.Bot.Messages.UserBuyMerch (botUser, merch));

                            botUser.AddPoints(75, Settings.Achivements.Buy);
                        }
                        catch
                        {
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.YouNotBuyMerch);
                        }
                        break;
                    default:
                        if (!executed)
                        {
                            var cmds = new Command[] { CommandController.GetCommand(Settings.Bot.CommandNames.Menu) };
                            botUser.AllowedCommands = cmds.ToList();
                            await BotController.SendMessage(user.Id, Settings.Bot.Messages.UnknownCommand,
                                BotController.GetKeyboardFromArray(cmds));
                        }
                        break;
                }
            }
        }

        #pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        public async static Task Error (ITelegramBotClient client, Exception ex, CancellationToken token) {
            Debug.LogCriticalError ($"Executing canceled!\n{ex.Message}", "Bot");
        }
    }
}