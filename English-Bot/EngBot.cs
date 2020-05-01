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
        //не обращайте внимания, я это забыл удалить
        public const string token_dima_test = @"91a6850da23e14a70147ff490504932c53fbcbf9c5f8f21e2f7c228949b3f2cf9c42a402e907aa123ce87";
        public const string url_dima_test = @"https://vk.com/ewb_test";

        //Mike
        //public const string Url = @"https://vk.com/club188523184";
        //public const string Token = "41df7d2e30314de0f847a51c4f8beaaf8d287eda3e31527d44a3d7aa17dac4928b9dc16be8ee476c64916";

        //менять только для смены паблика
        public static string Token = Resources.AccessToken;
        public static string Url = Resources.groupUrl;

        public static WordsDictionary dictionary = new WordsDictionary();
        public static Users users = new Users();//подгрузку из файла нужно сделать(или из Resources)
        public static VkBot bot = new VkBot(Token, Url, longPollTimeoutWaitSeconds: 0);
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

            /*
            SendSound(203654426, dictionary.eng_ids["working"]);
            SendSound(203654426, dictionary.eng_ids["good"]);
            SendSound(203654426, dictionary.eng_ids["staff"]);
            */

            WriteLine("Bot started!");
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            users.Save();
        }
    }
}
