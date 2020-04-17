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
        public static WordsDictionary dictionary = new WordsDictionary();
        public static Users users = new Users();//подгрузку из файла нужно сделать(или из Resources)
        public static VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl, longPollTimeoutWaitSeconds: 0);
        public static HashSet<long> adminIDs = new HashSet<long> { 122402184, 203654426, 210036813 };

        static void Main(string[] args)
        {
            //users.Load();

            ///Выполняется после закрытия программы 
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            //dictionary.Init_dict();  //заполнение словаря из 5000.txt

            //Testing_Start();     //Запуск тестирования

            bot.OnMessageReceived += NewMessageHandler;

            Thread botStart = new Thread(new ThreadStart(bot.Start));
            botStart.Start();
            //bot.Start();

            DailyEvent_start();         //Старт ежедневных событий

            //SendPicture(210036813, dictionary.GetEngWordId("inspector"));
            //SendPicture(210036813, dictionary.GetEngWordId("beautiful"));
            //SendPicture(210036813, dictionary.GetEngWordId("car"));

            WriteLine("Bot started!");

        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            users.Save();
        }
    }
}
