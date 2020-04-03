using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
//using VkBotFramework.Examples;
using VkNet.Model.RequestParams;
using English_Bot.Properties;
using static System.Console;
using System.Threading;

namespace English_Bot
{
    public partial class EngBot
    {
        static void DailyEvent_start()
        {
            Thread tr = new Thread(StartTimer);
            tr.Start();
        }

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
                Thread.Sleep(1000);
            }
        }

        ///<summary>
        ///метод ,который будет вызывать н раз отправку картинок 
        ///</summary>

        public static void Timer()
        {
            Random r = new Random();
            int TimesOfWork = r.Next(3, 11);
            int sleeptime = (int)Math.Ceiling((double)16 / TimesOfWork * 3600000);
            for (int i = 0; i < TimesOfWork; i++)
            {
                //if (users.Dbase != null && users.Dbase.Count != 0)
                foreach (var user in users.Dbase.Values)
                {
                    bool pic = r.Next(2) % 2 == 0;
                    if (pic)
                        SendPicture(user.userId, user.unLearnedWords.ElementAt(r.Next(user.unLearnedWords.Count)));
                    else
                        SendFullWordDescription(user.userId, user.unLearnedWords.ElementAt(r.Next(user.unLearnedWords.Count)));
                }
                if (DateTime.Now.Hour >= 23) break;
                Thread.Sleep(sleeptime);
                
            }
            
        }
    }
}
