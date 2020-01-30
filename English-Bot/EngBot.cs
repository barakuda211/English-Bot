using System;
using System.Linq;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using English_Bot.Properties;
using static System.Console;


namespace English_Bot
{
    public partial class EngBot
    {
        static Dictionary dictionary = new Dictionary();//подгрузку из файла нужно сделать
        static Users users = new Users();//подгрузку из файла нужно сделать(или из Resources)
        static VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl);

        static void Main(string[] args)
        {

            Testing_Start();     //Запуск тестирования

            bot.OnMessageReceived += NewMessageHandler;

            bot.Start();

            DailyEvent_start();         //Старт ежедневных событий

            WriteLine("Bot started!");
            
            ReadLine();
        }
        
    }
}
