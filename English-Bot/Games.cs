﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Crossword;
using System.Diagnostics;
using System.Net;
using English_Bot.Properties;
using static English_Bot.Timers;
using SpaceYandexEnRu;

namespace English_Bot
{
    public class Stopwatch
    {
        private DateTime start { get; set; }
        private DateTime stop { get; set; }

        public double ElapsedMilliseconds => (stop - start).TotalMilliseconds;

        public void Start()
        {
            start = DateTime.UtcNow;
        }

        public void Stop()
        {
            stop = DateTime.UtcNow;
        }

        public void Reset()
        {
            start = DateTime.MinValue;
            stop = DateTime.MinValue;
        }
        public Stopwatch() => Reset();


    }
    public static class Games
    {
        public static void Gallows_Start(long user_id)
        {
            EngBot.users[user_id].on_Test = true;
            Thread gallows_thread = new Thread(new ParameterizedThreadStart(Gallows_Thread_Start));
            gallows_thread.Start(user_id);
        } 

        static void Gallows_Thread_Start(object obj_id)
        {
            long user_id = (long)obj_id;

            // var g = new Gallows(user_id);
            EngBot.users[user_id].keyb = User.Gallows_KeyBoard;
            EngBot.SendMessage(user_id, "Это игра виселица, необходимо отгадать английское слово за ограниченное количество попыток!");
            EngBot.SendMessage(user_id, "Присылай мне по одной букве или пришли всё слово, если уже отгадал его", null, true);
            var gal = new Gallows(user_id);

            SendMessage(gal);
            if (Wait_answers_gallows(gal))
                EngBot.users[user_id].gall_passed++;
            EngBot.users[user_id].on_Test = false;
        }

        public static void Crossvord_start(long id)
        {
            EngBot.users[id].on_Test = true;
            Thread crossvord_thread = new Thread(new ParameterizedThreadStart(Crossvord_thread_start));
            crossvord_thread.Start(id);
        }

        static void Crossvord_thread_start(object Idobj)
        {
            long id = (long)Idobj;

            var scw = new SimpleCross(id);
            EngBot.users[id].keyb = User.Crossword1_Keyboard;
            EngBot.SendMessage(id, "Итак, твоя задача - перевести пронумерованные слова на английский.");
            EngBot.SendMessage(id, "Полученное слово, выделенное жёлтым, требуется перевести на русский.");
            EngBot.SendMessage(id, "Жду переводы по-порядку или ответы в виде:\n цифра перевод ",null,true);

            SendMessage(scw);

            if (Wait_normal_answers(scw))
                EngBot.users[id].cross_passed++;
            EngBot.users[id].on_Test = false;
        }

        static bool Wait_answers_gallows(Gallows gal)
        {
            int wait_time = 10;
            var ind = IndicatorTimer(wait_time);
            var user = EngBot.users[gal.user_id];

            string text = user.lastMsg.Item1.ToLower();
            long ident_msg = user.lastMsg.Item3;

            bool exit_by_hint = false; 

            while (true)
            {
                if (gal.attempts_remain == 0)
                {
                    user.keyb = User.Main_Keyboard; 
                    EngBot.SendMessage(gal.user_id, "Попытки закончились :(\nЗагаданное слово: " + gal.word, null, true);
                    return false;  
                }

                if (gal.success)
                {
                    user.keyb = User.Main_Keyboard; 
                    EngBot.SendMessage(gal.user_id, exit_by_hint ? "Вы отгадали слово с помощью подсказки, так не честно :)" : "Поздравляю! Вы выйграли!", null, true);
                    return exit_by_hint;
                }

                if (ind.x)
                {
                    user.keyb = User.Main_Keyboard;
                    EngBot.SendMessage(gal.user_id, "Ладно, тогда поиграем позже...", null, true);
                    return false; 
                }

                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);
                    continue; 
                }

                ind.x = true;
                ind = IndicatorTimer(wait_time);

                ident_msg = user.lastMsg.Item3;
                text = EngBot.GetFormatedWord(user.lastMsg.Item1);
                if (text == null || text.Length == 0)
                {
                    EngBot.SendMessage(gal.user_id, "Я жду текстовые ответы");
                    continue;
                }
                var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (text == "/hint" || text == "подсказать букву")
                {
                    EngBot.SendMessage(gal.user_id, "Вот подсказка ;)");
                    for (int i = 0; i < gal.word.Length; i++)
                        if (gal.show[i] == '?')
                        {
                            gal.OpenLetter(gal.word[i], true);
                            SendMessage(gal);
                            exit_by_hint = true; 
                            break;
                        }
                    continue;
                }

                if (text == "/give_up" || text == "я сдаюсь")
                {
                    user.keyb = User.Main_Keyboard;
                    EngBot.SendMessage(gal.user_id, @"Ну как хочешь :-\");
                    EngBot.SendMessage(gal.user_id, "Было загадано слово:\n" + gal.word.ToUpper(), null, true);
                    return false;
                }

                if (words.Length > 1)
                {
                    EngBot.SendMessage(gal.user_id, @"Что-то не так с количеством cлов :-\");
                    continue;
                }

                if (words[0].Length != 1)
                {
                    // EngBot.SendMessage(gal.user_id, @"Что-то не так с количеством букв :-\");
                    if (gal.word == words[0].ToLower())
                    {
                        user.keyb = User.Main_Keyboard;
                        EngBot.SendMessage(gal.user_id, @"Поздравляем! Вы выиграли!", null, true);
                        return true;
                    }
                    else
                    {
                        EngBot.SendMessage(gal.user_id, @"Вы не угадали слово :(");
                        gal.attempts_remain -= 1;
                        SendMessage(gal);
                    }
                    continue;
                }

                char c = words[0].ToLower()[0];

                if (!char.IsLetter(c))
                {
                    EngBot.SendMessage(gal.user_id, @"Это не буква :-/");
                    continue;
                }

                if (gal.used.Contains(c))
                {
                    EngBot.SendMessage(gal.user_id, "Эта буква уже отгадана!");
                    continue; 
                }

                if (gal.word.IndexOf(c) >= 0)
                {
                    gal.OpenLetter(c);
                    SendMessage(gal);
                }
                else
                {
                    EngBot.SendMessage(gal.user_id, "Такой буквы в слове нет");
                    --gal.attempts_remain;
                    SendMessage(gal);
                }
            }
        }

        static bool Wait_normal_answers(SimpleCross scw)
        {
            int wait_time = 5;
            var ind = IndicatorTimer(wait_time);

            long userID = scw.id;
            var user = EngBot.users[scw.id];
            string text = user.lastMsg.Item1.ToLower();
            long ident_msg = user.lastMsg.Item3;
            while (true)
            {
                if (ind.x)
                {
                    user.keyb = User.Main_Keyboard;
                    EngBot.SendMessage(userID, "Ладно, тогда потом поиграем...",null,true);
                    return false;
                }
                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);
                    continue;
                }

                ind.x = true;
                ind = IndicatorTimer(wait_time);

                ident_msg = user.lastMsg.Item3;
                text = EngBot.GetFormatedWord(user.lastMsg.Item1);
                if (text == null || text.Length == 0)
                {
                    EngBot.SendMessage(userID, "Я жду текстовые ответы");
                    continue;
                }
                var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (text == "/hint" || text == "подсказать слово")
                {
                    EngBot.SendMessage(userID, @"Ну как хочешь :-\");
                    for (int i = 0; i < scw.is_answered.Count; i++)
                        if (!scw.is_answered[i])
                        {
                            scw.DrawWord(i);
                            SendMessage(scw);
                            break;
                        }
                    if (scw.is_all_answered)
                        break;
                    continue;
                }

                if (text == "/give_up" || text == "я сдаюсь")
                {
                    user.keyb = User.Main_Keyboard;
                    EngBot.SendMessage(userID, @"Ну как хочешь :-\",null,true);
                    scw.DrawWords();
                    SendMessage(scw);
                    return false;
                }

                if (words.Length == 1)
                {
                    int i = scw.is_answered.FindIndex(x => !x);
                    if (i >= scw.words.Count || i < 0)
                        break;
                    if (text != scw.words[i].Item1)
                    {
                        EngBot.SendMessage(userID, "Ошибочка, попробуй ещё раз.");
                        continue;
                    }
                    EngBot.SendMessage(userID, "Отлично");
                    scw.DrawWord(i);
                    SendMessage(scw);

                    if (scw.is_all_answered)
                        break;
                    continue;
                }

                if (words.Length >2)
                {
                    EngBot.SendMessage(userID, @"Что-то не так с количеством слов :-\");
                    continue;
                }
                int num = -1;
                if (!int.TryParse(words[0], out num))
                {
                    EngBot.SendMessage(userID, $" \"{words[0]}\" - это точно цифра?)");
                    continue;
                }
                if (num < 1 || num > scw.words.Count)
                {
                    EngBot.SendMessage(userID, "Такого номера я не вижу...");
                    continue;
                }
                if (scw.is_answered[num - 1])
                {
                    EngBot.SendMessage(userID, "Это слово уже отгадано.");
                    continue;
                }
                if (scw.words[num - 1].Item1 != words[1])
                {
                    EngBot.SendMessage(userID, "Ошибочка, попробуй ещё раз.");
                    continue;
                }
                EngBot.SendMessage(userID, "Отлично");
                scw.DrawWord(num - 1);

                SendMessage(scw);
     
                if (scw.is_all_answered)
                    break;
            }

            user.keyb = User.Crossword2_Keyboard;
            EngBot.SendMessage(userID, "Супер, так что же такое "+scw.MainWord.Item1+"?",null,true);

            //string ans = EngBot.dictionary[scw.MainWord.Item2].rus;
            List<string> ans = new List<string>();
            foreach (var def in EngBot.dictionary[scw.MainWord.Item2].mean_rus.def)
                ans.Add(def.tr[0].text);

            while (true)
            {
                if (ind.x)
                {
                    user.keyb = User.Main_Keyboard;
                    EngBot.SendMessage(userID, "Ладно, тогда потом поиграем...",null,true);
                    return false;
                }
                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);
                    continue;
                }

                ind.x = true;
                ind = IndicatorTimer(wait_time);
                ident_msg = user.lastMsg.Item3;
                text = EngBot.GetFormatedWord(user.lastMsg.Item1);
                if (text == null || text.Length == 0)
                {
                    EngBot.SendMessage(userID, "Я жду текстовые ответы");
                    continue;
                }
                if (text == "/give_up" || text == "я сдаюсь")
                {
                    user.keyb = User.Main_Keyboard;
                    string s = $"Стыдно не знать, это же ";
                    for (int i = 0; i < ans.Count;i++)
                    {
                        s += ans[i];
                        if (i != ans.Count - 1)
                            s += @"/";
                    }
                    EngBot.SendMessage(userID, s);
                    EngBot.SendMessage(userID, $"В следующий раз повтори слова тщательней)",null,true);
                    return false;
                }

                if (!ans.Contains(text))
                {
                    EngBot.SendMessage(userID, "Ошибочка, попробуй ещё раз.");
                    continue;
                }
                break;
            }
            user.keyb = User.Main_Keyboard;
            EngBot.SendMessage(userID, "Правильно, поздравляю!",null,true);
            return true;
        } 

        static void SendMessage(Gallows gal)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();

            string message = "Слово: " + string.Join("", gal.show).ToUpper() + "\n" +
                "Количество попыток: " + gal.attempts_remain + "\n" +
                "Использованные буквы: " + string.Join(", ", gal.used) + "\n" +
                // "Часть речи: " + EngBot.dictionary[gal.word_id]; 
                "Перевод: " + string.Join(", ", gal.tr); 


            try
            {
                EngBot.bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = gal.user_id,
                    Message = message
                });
                stp.Stop();
            }
            catch (VkNet.Exception.TooMuchOfTheSameTypeOfActionException)
            {
                Console.WriteLine("VK poshel v zhopu");
            }
            catch (VkNet.Exception.PublicServerErrorException)
            {
                Console.WriteLine("Server error with sending message!");
            }
            catch (VkNet.Exception.CannotSendToUserFirstlyException)
            {
                Console.WriteLine("Server error with sending message!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Some error with sending cross");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.WriteLine("Elapsed for sending: " + stp.ElapsedMilliseconds);
        }

        static void SendMessage(SimpleCross scw)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();

            long id = scw.id;
            string legend = "";
            for (int i = 0; i < scw.words.Count; i++)
                if (!scw.is_answered[i])
                    legend = legend + scw.legend[i];


            string url = EngBot.bot.Api.Photo.GetMessagesUploadServer(id).UploadUrl;

            var uploader = new WebClient();
            var uploadResponseInBytes = uploader.UploadFile(url, @"users\" + id + @"\cross.jpg");
            var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
            // VKRootObject response = Methods.DeSerializationObjFromStr<VKRootObject>(uploadResponseInString);
            var photos = EngBot.bot.Api.Photo.SaveMessagesPhoto(uploadResponseInString);
            try
            {
                EngBot.bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = id,
                    Message = legend,
                    Attachments = photos
                });

                stp.Stop();
            }
            catch (VkNet.Exception.TooMuchOfTheSameTypeOfActionException)
            {
                Console.WriteLine("VK poshel v zhopu");
            }
            catch (VkNet.Exception.PublicServerErrorException)
            {
                Console.WriteLine("Server error with sending message!");
            }
            catch (VkNet.Exception.CannotSendToUserFirstlyException)
            {
                Console.WriteLine("Server error with sending message!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Some error with sending gallow");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.WriteLine("Elapsed for sending: " + stp.ElapsedMilliseconds);
        }

        public static void PlayCrossword(long user_id)
        {
            CrossMaker CM = new CrossMaker();
            //CM.word_list = new List<long>();
            foreach (long word_id in EngBot.users[user_id].unLearnedWords)
            {
                CM.word_list.Add(word_id);
            }
            CM.CrosswordMaker();

            char[,] field = new char[10, 10];
            foreach (var cross in CM.cross)
            {
                if (cross.direction == Cross.Direction.toRight)
                {
                    int x = cross.pos.x;
                    int y = cross.pos.y;
                    foreach (char c in EngBot.dictionary[cross.word_id].eng)
                    {
                        field[x, y] = c;
                        ++x;
                    }
                }
                else
                {
                    int x = cross.pos.x;
                    int y = cross.pos.y;
                    foreach (char c in EngBot.dictionary[cross.word_id].eng)
                    {
                        field[x, y] = c;
                        ++y;
                    }
                }
            }

            string result = "";
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                    result += field[i, j];
                result += "\n";
            }

            EngBot.SendMessage(user_id, result);
        }

    }
}
