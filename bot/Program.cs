/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using System;
using Core;
using TeleBot;
using Telegram.Bot.Types;

namespace bot
{
    class Program
    {
        static void Main(string[] args)
        {
            BotController.RestartBot();

            while (true)
            {
                Debug.Log("Command list:\n" +
                    "Log - change logging\n" +
                    "Restart - restart bot");
                var userCmd = Console.ReadLine();
                var executed = false;
                foreach (Command cmd in CommandController.BotCommands)
                {
                    if (userCmd == cmd.Name)
                    {
                        cmd.Execute(new User() { Id = Settings.Bot.AdminsId[0] });
                        executed = true;
                        break;
                    }
                }
                if (!executed)
                {
                    switch (userCmd)
                    {
                        case "Log":
                            Settings.Debug.ChangeLogging();
                            Debug.Log(Settings.Debug.AllowLogging.ToString());
                            break;
                        case "Restart":
                            BotController.RestartBot();
                            Debug.Log(Settings.Debug.AllowLogging.ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
