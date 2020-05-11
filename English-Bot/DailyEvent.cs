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
using VkNet.Enums.Filters;
using Dictionary;

namespace English_Bot
{
    public partial class EngBot
    {
        // Один час
        public const int OneHour = 3600000;

        // True, если идет рассылка сообщений пользователям
        public static bool Sending_Words_Goes; 

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
                // Methods.DeSerializationObjFromStr<>(VkApi.VkRequests.Request("https://api.vk.com/method/friends.get?user_id=" + 122402184)); 
                List<(long id, string name)> friends = bot.Api.Friends.Get(new FriendsGetParams { UserId = userID }, true).Where(x => users.Dbase.Keys.Contains(x.Id)).Select(x => (x.Id, x.FirstName + " " + x.LastName)).ToList();
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
            int TimeNowHour;
            while (true)
            {
                TimeNowHour = DateTime.Now.Hour;
                if /*(TimeNowHour == 10)*/ ((TimeNowHour >= 10) && (TimeNowHour < 20) && !Sending_Words_Goes)
                    WordsSender();
                if (TimeNowHour == 23)
                    users.Save(); 
                Thread.Sleep(OneHour);
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
                {
                    if (users.Dbase != null && users.Dbase.Count != 0)
                        foreach (var user in users.Dbase.Values)
                            if (!user.on_Test && !user.bot_muted)
                                Testing_Start(user.userId);
                }
                /*
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
                }*/ 
                Thread.Sleep(OneHour);
                Time = DateTime.Now.Hour;
            }
        }

        ///<summary>
        /// Отправляет пользователям картнку, описание и пример использования слова один раз в час 
        ///</summary>
        public static void WordsSender()
        {
            Sending_Words_Goes = true; 
            Random r = new Random();
            int TimesOfWork = Users.UNLearned;
            int sleeptime = OneHour; 
            for (int i = 0; i < TimesOfWork; i++)
            {
                foreach (var user in users.Dbase.Values /* .Where(x => x.userId == 203654426) */ )
                {
                    if (user.on_Test || user.bot_muted || i + 1 > user.day_words)
                        continue; 

                    SendWord(user.userId, user.unLearnedWords.ElementAt(i));
                }
                if (DateTime.Now.Hour == 19)
                {
                    Sending_Words_Goes = false;
                    break;
                }
                Thread.Sleep(sleeptime);
            }
        }
    }
}
