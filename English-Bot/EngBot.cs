using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
//using VkBotFramework.Examples;
using VkNet.Model.RequestParams;
using English_Bot.Properties;
using static System.Console;
using System.Threading;
using System.Threading.Tasks;

namespace English_Bot
{
    class EngBot
    {
        ///<summary>
        ///вспомогатаельный метод ,который проверяет текущее время 
        /// и вызывает основной метод
        ///</summary>
        public static void StartTimer()
        {
            var TimeNowHour = DateTime.Now.Hour;
            while (true)
            {
                if ((TimeNowHour >= 7) && (TimeNowHour) < 23)
                    Timer();
                TimeNowHour = DateTime.Now.Hour;
            }
        }

        ///<summary>
        ///метод ,который будет вызывать н раз метод тестирования 
        ///</summary>
     
        public static void Timer()
        {
            Random r = new Random();
            int TimesOfWork = r.Next(3, 11);
            int sleeptime = 16/TimesOfWork*360000;
            for (int i = 0; i < TimesOfWork; i++)
            {
                // вызвать метод для тестирования 
                if(DateTime.Now.Hour>=23) break;
                Thread.Sleep(sleeptime);
            }
        }


        static void Main(string[] args)
        {
            //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();
            //ILogger<VkBot> logger = loggerFactory.CreateLogger<VkBot>();

            //ExampleSettings settings = ExampleSettings.TryToLoad(logger);

            VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl);

            bot.OnMessageReceived += NewMessageHandler;

            bot.Start();

            Thread tr = new Thread(StartTimer);
            tr.Start();

            WriteLine("Bot started!");
            ReadLine();
        }
        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {

            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId;
            var fromId = eventArgs.Message.FromId;
            var text = eventArgs.Message.Text;

            instanse.Logger.LogInformation($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
            instanse.Api.Messages.Send(new MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                PeerId = eventArgs.Message.PeerId,
                Message =
                    $"{fromId.Value}, i have captured your message: '{text}'. its length is {text.Length}. number of spaces: {text.Count(x => x == ' ')}"
            });
        }
    }
}
