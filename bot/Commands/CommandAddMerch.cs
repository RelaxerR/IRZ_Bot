using System;
/* 
* Created by RelaxerR
* Please, leave this message, if you use this code.
* My social networks
* Telegram: https://t.me/relaxerr_teech
* VK: https://vk.com/relaxerrprod
*/
using TeleBot;
using Core;
using Telegram.Bot.Types;

namespace Commands
{
    class CommandAddMerch : Command
    {
        public override string Name => Settings.Bot.CommandNames.AddMerch;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            BotUserController.GetUser(user).MyState = BotController.State.AddMerch;
            await BotController.SendMessage(user.Id,
                Settings.Bot.Messages.AddMerch,
                BotController.GetKeyboardFromArray(AllowedCommands));
        }
    }
}
