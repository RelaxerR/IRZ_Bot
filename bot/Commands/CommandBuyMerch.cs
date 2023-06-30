using System;
using System.Collections.Generic;
using System.Text;
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
    class CommandBuyMerch : Command
    {
        public override string Name => Settings.Bot.CommandNames.BuyMerch;

        public override Command[] AllowedCommands { get; set; }

        public override async void Execute(User user)
        {
            StartExecute(user);
            var merchList = Data.GetMerchList();
            var txt = $"{Settings.Bot.Messages.MerchListName}\n\n";
            for (int i = 0; i < merchList.Length; i++)
            {
                var m = merchList[i];
                txt += $"{i} - {m} - {Data.GetMerchCost(m)}{Settings.Bot.Messages.PointsName}\n";
            }
            BotUserController.GetUser(user).MyState = BotController.State.ChooseMerch;
            await BotController.SendMessage(user.Id,
                txt,
                BotController.GetKeyboardFromArray(AllowedCommands));;
        }
    }
}
