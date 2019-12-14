using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
//using VkBotFramework.Examples;
using VkNet.Model.RequestParams;
using English_Bot.Properties;
using static System.Console;

namespace English_Bot
{
    partial class EngBot
    {
        static void Main(string[] args)
        {
            VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl);

            bot.OnMessageReceived += NewMessageHandler;
            WriteLine("Bot started!");
            bot.Start();

            
            ReadLine();
        }
        
    }
}
