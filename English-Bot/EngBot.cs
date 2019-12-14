using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using English_Bot.Properties;
using static System.Console;

namespace English_Bot
{
    partial class EngBot
    {
        //Список пользователей
        static public Users UsersDictionary = new Users();

        static void Main(string[] args)
        {
            VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl);

            InitUsersDict();

            bot.OnMessageReceived += NewMessageHandler;
            WriteLine("Bot started!");
            bot.Start();

            
            ReadLine();
        }

        //Заполнение списка пользователей из json
        static void InitUsersDict()
        {
            //TODO
        }

    }
}
