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
        /// <summary>
        /// Старт ежедневных событий
        /// </summary>
        static void DailyEvent_start()
        {
            Thread tr = new Thread(StartTimer);
            Thread test = new Thread(TestStart);
            tr.Start();
            test.Start();
        }

        public static void ShowSuccess(long userID)
        {
            try
            {
                string message = "Поздравляем! За неделю Вы выучили " + users[userID].week_words + "\n";
                List<(long id, string name)> friends = bot.Api.Friends.Get(new FriendsGetParams() { UserId = userID }).Where(x => users.Dbase.Keys.Contains(x.Id)).Select(x => (x.Id, x.FirstName + " " + x.LastName)).ToList();
                message += "Вот топ пять пользователей среди Вас и ваших друзей по итогам недели: \n";
                friends.Add((userID, "Вы"));
                var sorted = friends.OrderByDescending(x => users[x.id].week_words);
                foreach (var user in sorted)
                {
                    message += user.name + " -> " + users[user.id].week_words + "\n";
                }
                SendMessage(userID, message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something bad with sending statistics");
                Console.WriteLine(e.Message);
            }
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
                if (TimeNowHour == 10) //((TimeNowHour >= 10) && (TimeNowHour) < 20)
                    WordsSender();
                TimeNowHour = DateTime.Now.Hour;
                Thread.Sleep(3600000);
            }
        }

        /// <summary>
        /// Запуск ежедневного тестирования вечером
        /// </summary>
        public static void TestStart()
        {
            var Time = DateTime.Now.Hour;
            while (true)
            {
                if (Time == 20)
                    if (users.Dbase != null && users.Dbase.Count != 0)
                        foreach (var user in users.Dbase.Values)
                        {
                            Testing_Start(user.userId);
                        }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && Time == 21)
                {
                    foreach (var user in users.Dbase.Values)
                    {
                        ShowSuccess(user.userId);
                    }
                    foreach (var user in users.Dbase.Values)
                    {
                        users[user.userId].week_words = 0;
                    }
                }
                Thread.Sleep(3600000);
                Time = DateTime.Now.Hour;
            }
        }

        ///<summary>
        ///метод ,который будет вызывать н раз отправку картинок 
        ///</summary>

        public static void WordsSender()
        {
            Random r = new Random();
            int TimesOfWork = Users.UNLearned;
            int sleeptime = 3600000; //(int)Math.Ceiling((double)10 / TimesOfWork * 3600000);
            for (int i = 0; i < TimesOfWork; i++)
            {
                if (users.Dbase != null && users.Dbase.Count != 0)
                    foreach (var user in users.Dbase.Values)
                    {
                        if (user.on_Test)
                            continue;
                        bool pic = r.Next(2) % 2 == 0;
                    Desc:
                        if (pic)
                        {
                            bool success = SendPicture(user.userId, user.unLearnedWords.ElementAt(i /*r.Next(user.unLearnedWords.Count)*/));
                            if (!success)
                            {
                                pic = false;
                                goto Desc;
                            }
                        }
                        else
                        {
                            SendFullWordDescription(user.userId, user.unLearnedWords.ElementAt(i /*r.Next(user.unLearnedWords.Count)*/));
                        }
                    }
                if (DateTime.Now.Hour == 19) break;
                Thread.Sleep(sleeptime);
            }
            
        }
    }
}
