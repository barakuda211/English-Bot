using System;
using System.Linq;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using System.Threading;
using English_Bot.Properties;
using static System.Console;


namespace English_Bot
{
    public partial class EngBot
    {
        static Dictionary dictionary = new Dictionary();
        static Users users = new Users();//подгрузку из файла нужно сделать(или из Resources)
        static VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl, longPollTimeoutWaitSeconds: 0);

        static void Main(string[] args)
        {
            users.Load();

            ///Выполняется после закрытия программы 
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            Testing_Start();     //Запуск тестирования

            bot.OnMessageReceived += NewMessageHandler;

            Thread botStart = new Thread(new ThreadStart(bot.Start));
            botStart.Start();
            //bot.Start();

            //DailyEvent_start();         //Старт ежедневных событий

            WriteLine("Bot started!");

        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            users.Save();
        }
    }
}
