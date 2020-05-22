using System;
using System.Linq;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using System.Threading;
using English_Bot.Properties;
using static System.Console;
using System.Diagnostics;
using System.IO;

namespace English_Bot
{
    public partial class EngBot
    {
        
        //не обращайте внимания, я это забыл удалить
        public const string token_dima_test = @"302865fe4032238938c1bb36476fe443ada8fb8ce1010880635e6fb1ce0ba6da8d0e34357fa3086360109";
        public const string url_dima_test = @"https://vk.com/ewb_test";
        public const string id_dima_test = "194325372";

        //Mike
        //public const string Url = @"https://vk.com/club188523184";
        //public const string Token = "41df7d2e30314de0f847a51c4f8beaaf8d287eda3e31527d44a3d7aa17dac4928b9dc16be8ee476c64916";

        //менять только для смены паблика
        public static string Token = token_dima_test;
        public static string Url = url_dima_test;

        public static WordsDictionary dictionary = new WordsDictionary();
        public static Users users = new Users();//подгрузку из файла нужно сделать(или из Resources)
        public static VkBot bot = new VkBot(Token, Url, longPollTimeoutWaitSeconds: 0);
        public static HashSet<long> adminIDs = new HashSet<long> { 122402184, 203654426, 210036813 };

        public static string reboot_argument = "not_force";

        static void Main(string[] args)
        {
            try
            {
                ///Выполняется после закрытия программы 
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                bot.OnMessageReceived += NewMessageHandler;

                Thread botStart = new Thread(new ThreadStart(bot.Start));
                botStart.Start();

                SendRestart();

                DailyEvent_start();         //Старт ежедневных событий

                WriteLine("Bot started!");

            }
            catch (Exception e)
            {
                string message = "Бот упал\n" + e.Message + "\n" + e.StackTrace;
                users.Save();
                SaveFailureMain(message);
                SendFailure();
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            users.Save();
            SaveFailureVk("Бот упал\n" + e.ToString());
            SendFailure();
        }

        public static void SendRestart()
        {
            try
            {
                foreach (var user in adminIDs)
                {
                    SendMessage(user, "Я успешно перезагрузился");
                }
            }
            catch(Exception e)
            {
                WriteLine("Не удалось отправить сообщение о перезагрузке");
                WriteLine(e.Message);
                WriteLine(e.StackTrace);
            }
        }

        public static void SendFailure()
        {
            try
            {
                foreach (var admin in adminIDs)
                {
                    SendMessage(admin, "Я упал, начинаю перезагрузку");
                }
            }
            catch (Exception e)
            {
                WriteLine("Не удалось отправить сообщения об ошибке");
                WriteLine(e.Message);
                WriteLine(e.StackTrace);
            }
        }

        public static void SaveFailureVk(string message)
        {
            try
            {
                File.WriteAllText("bot_error_vk.txt", message);
            }
            catch (Exception e)
            {
                WriteLine("Не удалось сохранить сообщение об ошибке");
            }
        }

        public static void SaveFailureMain(string message)
        {
            try
            {
                File.WriteAllText("bot_error_main.txt", message);
            }
            catch(Exception e)
            {
                WriteLine("Не удалось сохранить сообщение об ошибке"); 
            }
        }
    } 
}
