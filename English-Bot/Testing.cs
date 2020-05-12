using System;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
using System.Linq;
using static System.Console;
using System.Threading;
using Project_Word;
using VkApi;
using static System.Math;

namespace English_Bot
{
    public partial class EngBot
    {
        /*
        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {

            var fromId = eventArgs.Message.FromId;
            var text = eventArgs.Message.Text;
            var peerId = eventArgs.Message.PeerId;
            VkBot instanse = sender as VkBot;

            if (users.GetUser(fromId.Value) == null)
                users.AddUser(new User(fromId.Value, 0, new HashSet<string>(), new HashSet<long>(), new HashSet<long>()));//добавляет пользователя, если его не было в users
            
            users.GetUser(fromId.Value).lastMsg = (text.ToLower(), false, eventArgs.Message.ConversationMessageId.Value);

            WriteLine($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
        }
        */

        //отправляет сообщение юзеру
        public static void SendMessage(long userID, string message, long[] msgIDs = null, bool need_kb = false)
        {
            try
            {
                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = userID,
                    Message = message,
                    ForwardMessages = msgIDs,
                    Keyboard = need_kb ? users[userID].keyb.ToMessageKeyboard() : null
                });
            }
            catch (VkNet.Exception.TooMuchOfTheSameTypeOfActionException)
            {
                WriteLine("VK poshel v zhopu");
            }
            catch (VkNet.Exception.PublicServerErrorException)
            {
                WriteLine("Server error with sending message!");
            }
            catch (VkNet.Exception.CannotSendToUserFirstlyException)
            {
                WriteLine("Server error with sending message!");
            }
            WriteLine("Message sent to " + userID);
        }

        /// reduce upper register and deleting 'ё'
        public static string GetFormatedWord(string word) => string.Join("", word.Trim().Select(x => char.ToLower(x))).Replace('ё', 'е');

        //ждет ответа !определенного! ответа от юзера, 
        //always отвечает за время ожидания(false - 1 попытка, true - ждет, пока юзер не напишет нужное)
        static long WaitWordFromUser(long userID, string[] word, bool always, long word_id)
        {
            var user = users.GetUser(userID);
            if (always)
            {
                while (word.All(x => x.ToLower() != user.lastMsg.Item1.ToLower()) || user.lastMsg.Item2) Thread.Sleep(100);  //ожидание согласия
                WriteLine("Get \"Ready\"");
                user.lastMsg.Item2 = true;
            }
            if (!always)
            {
                while (user.lastMsg.Item2 != false) Thread.Sleep(100);
                WriteLine("get word");
                WriteLine(user.lastMsg.Item3);
                user.lastMsg.Item2 = true;
                user.lastMsg.Item1 = GetFormatedWord(user.lastMsg.Item1);
                return (word.Any(x => x == user.lastMsg.Item1) ? -word_id : user.lastMsg.Item3);
            }
            return 0;//заглушка
        }

        // static int TEST_Words = 5; 

        //тестирование пользователя по !10! последним неизученным словам
        static void Testing(object IDobj)
        {
            (long, bool) val = ((long, bool))IDobj;
            long userID = val.Item1;
            bool repeat = val.Item2;
            //Console.WriteLine("Number of words = " + users.GetUser(userID).unLearnedWords.Count);
            SendMessage(userID, "Вам будет предложен тест на знание английских слов. " +
                                "Не стоит подсматривать, от результатов теста зависит ваша дальнейшая программа обучения. " +
                                "Переводите слова наиболее близким по вашему мнению словом. ");
            if (users[userID].tests_passed == 0)
                SendMessage(userID, "Жду вашей команды: \"Готов\". ", null, true);
            else
                SendMessage(userID, "Жду вашей команды: \"Готов\" или \"Не готов\". ", null, true);
            List<string> agree = new List<string>();
            agree.Add("готов");
            agree.Add("да");
            agree.Add("точно");


            if (users[userID].tests_passed == 0)
            {
                if (!WaitAgreeFromUser_Timer(userID, agree.ToArray(), new string[] { }, 15, "Ладно, пиши, когда будешь готов."))
                {
                    users.DeleteUser(userID);
                    users.Save();
                    return;
                }
            }
            else
                if (!WaitAgreeFromUser_Timer(userID, agree.ToArray(), new string[] { "нет", "не готов", "потом" }, 60, "Ладно, протестируемся потом."))
            {
                users[userID].on_Test = false;
                return;
            }

            var rand = new Random();

            HashSet<long> lastULW = new HashSet<long>();

            if (repeat)
                foreach (var id in users[userID].learnedWords.OrderBy(x => rand.Next(users[userID].learnedWords.Count)).Take(users[userID].day_words))
                    lastULW.Add(id);
            else
                foreach (var word in users[userID].unLearnedWords)
                    lastULW.Add(word);

            //лист для проверки ответов
            List<long> msgIDs = new List<long>();

            foreach (long idx in lastULW)//присылает и ждет ответ на последние изученные слова
            {
                var word = dictionary.GetWord(idx);
                // 0 - Английское слово 
                // 1 - Русское слово 
                int r = rand.Next(2);

                if (users[userID].mode == Users.Mode.Easy)
                {
                    if (r == 0 /* && word.mean_rus != null */)
                    {
                        var list = dictionary.GetRandomRusWords(3, users[userID].userLevel);
                        list.Add(word.rus);
                        list = list.OrderBy(x => rand.Next(10)).ToList();
                        Keyboard Easy_Testing_Keyboard = new Keyboard(new Button[][] { new Button[] { new Button(list.ElementAt(0)), new Button(list.ElementAt(1)) }, new Button[] { new Button(list.ElementAt(2)), new Button(list.ElementAt(3)) } });
                        users[userID].keyb = Easy_Testing_Keyboard;
                        SendMessage(userID, word.eng, null, true);
                    }
                    else
                    {
                        var list = dictionary.GetRandomEngWords(3, users[userID].userLevel);
                        list.Add(word.id);
                        list = list.OrderBy(x => rand.Next(10)).ToList();
                        Keyboard Easy_Testing_Keyboard = new Keyboard(new Button[][] { new Button[] { new Button(dictionary[list.ElementAt(0)].eng), new Button(dictionary[list.ElementAt(1)].eng) }, new Button[] { new Button(dictionary[list.ElementAt(2)].eng), new Button(dictionary[list.ElementAt(3)].eng) } });
                        users[userID].keyb = Easy_Testing_Keyboard;
                        SendMessage(userID, word.rus, null, true);
                    }
                }
                else
                {
                    SendMessage(userID, (r == 0 /*&& word.mean_rus != null)*/ ? word.eng : word.rus));
                }

                List<string> wrds = new List<string>();
                if (r == 1 /*|| word.mean_rus == null*/)
                {
                    wrds.Add(word.eng);
                    if (dictionary.rus_ids.ContainsKey(word.rus))
                        wrds.AddRange(dictionary.rus_ids[word.rus].Select(x => dictionary[x].eng));
                }
                else
                {
                    //if (word.rus != null)
                    wrds.Add(word.rus);
                    //if (word.mean_rus != null)
                    foreach (var def in word.mean_rus.def)
                        foreach (var tr in def.tr)
                        {
                            wrds.Add(tr.text);
                            if (tr.syn != null)
                                wrds.AddRange(tr.syn.Select(x => x.text));
                        }
                }
                // wrds.AddRange(wrds);
                msgIDs.Add(WaitWordFromUser(userID, wrds.ToArray(), false, idx));
            }

            if (users[userID].learnedWords == null && msgIDs.Any(x => x < 0))
                users[userID].learnedWords = new HashSet<long>();

            WriteLine("Words learnt");
            int good_words = msgIDs.FindAll(x => x < 0).Count;
            SendMessage(userID, $"Вы ответили на {good_words} из {lastULW.Count()}. ");
            users[userID].week_words += good_words;

            // Добавляем верные ответы в изученные слова и убираем из невыученных
            if (!repeat)
                foreach (var id in msgIDs.FindAll(x => x < 0))
                {
                    try
                    {
                        users[userID].learnedWords.Add(-id);
                        users[userID].unLearnedWords.Remove(-id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Something wrong in removing unlearned or adding learned words");
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }

            //исправление ошибок юзера
            if (msgIDs.FindAll(x => x < 0).Count() < lastULW.Count()) //если есть ошибки
            {
                SendMessage(userID, "Вы ошиблись в следующем:");
                /*
                foreach (var x in msgIDs)
                    WriteLine(x);
                foreach (var x in lastULW)
                    WriteLine(x);
                */
                // long[] aError = new long[1];
                foreach (var pnt in msgIDs.Zip(lastULW, (x, y) => new { A = x, B = y }))
                {
                    if (pnt.A > 0)//идет по ошибкам
                    {
                        var temp = dictionary.GetWord(pnt.B);
                        //aError[0] = pnt.A;//массив с 1 пересланным сообщением, где юзер сделал ошибку
                        SendMessage(userID, $"\n{temp.eng} - { Translation(temp.eng) /*(temp.rus == null ? "?" : temp.rus)*/}"/*, aError*/);
                    }
                }
            }
            Console.WriteLine("Errata shown --------------------");

            if (repeat)
            {
                goto End;
            }

            // if first prob 
            if (users[userID].tests_passed == 0)
            {
                int lev = (int)Math.Floor((double)msgIDs.FindAll(x => x < 0).Select(x => dictionary[-x].level).Sum() / 9);
                users[userID].userLevel = lev == 5 ? lev : lev + 1;
                SendMessage(userID, "Отличное начало! \nВаш уровень - " + users[userID].userLevel + " из 5");
                Console.WriteLine("First prob successful! ----------");
                goto First;
            }

            List<long> words_level = dictionary.GetKeysByLevel(users[userID].userLevel).Where(x => !users[userID].learnedWords.Contains(x) && !users[userID].unLearnedWords.Contains(x) && dictionary[x].mean_rus != null).ToList();

            if (words_level.Count == 0 && users[userID].unLearnedWords.Count == 0)
            {
                if (users[userID].userLevel == 5)
                {
                    users[userID].userLevel = -1;
                    SendMessage(userID, "Поздравляем! Вы изучили 10000 слов на уровне 5 и открыли новый огромный список слов!");
                }
                else if (users[userID].userLevel == -1)
                {
                    SendMessage(userID, "Невероятно! Вы изучили все слова, которые знает бот!!!");
                    Fin(userID);
                    return;
                }
                else
                {
                    users[userID].userLevel++;
                    SendMessage(userID, "Поздравляем, Вы перешли на уровень " + users[userID].userLevel);
                }
                Console.WriteLine("Level changed! ----------");
            }
            else { goto Next; }

        First:
            words_level = dictionary.GetKeysByLevelWithTr(users[userID].userLevel).Where(x => !users[userID].learnedWords.Contains(x) && !users[userID].unLearnedWords.Contains(x)).ToList();

        Next:
            while (users[userID].unLearnedWords.Count < Users.UNLearned)
            {
                if (words_level.Count == 0)
                    break;
                int value = rand.Next(words_level.Count);
                users[userID].unLearnedWords.Add(words_level.ElementAt(value));
                words_level.RemoveAt(value);
            }
            Console.WriteLine("Words added to 10 ---------------");

        End:
            Fin(userID);
        }

        static void Fin(long id)
        {
            users[id].on_Test = false;
            users[id].tests_passed++;
            users.Save();
            users[id].keyb = User.Main_Keyboard;
            SendMessage(id, "Завтра продолжим :)", null, true);
        }

        static void Testing_Start(long id, bool repeat)
        {
            var user = users[id];
            user.on_Test = true;
            if (user.keyb == User.Main_Keyboard)
                user.keyb = User.ReadyOrNot_Keyboard;
            Thread testingThread = new Thread(new ParameterizedThreadStart(Testing));
            testingThread.Start((id, repeat));
        }

        static bool WaitAgreeFromUser_Timer(long userID, string[] agree, string[] deny, int wait_time, string wait_message, string wrong_ans = "Не понял")
        {
            var user = users.GetUser(userID);

            var ind = Timers.IndicatorTimer(wait_time);
            long ident_msg = user.lastMsg.Item3;
            while (true)
            {
                if (ind.x)
                {
                    if (user.unLearnedWords.Count != 15)
                        user.keyb = User.Main_Keyboard;
                    SendMessage(userID, wait_message,null,true);
                    return false;
                }
                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);  //ожидание согласия
                    continue;
                }
                ident_msg = user.lastMsg.Item3;
                string text = GetFormatedWord(user.lastMsg.Item1);

                if (agree.Any(x => x == text)) 
                    break;
                if (deny.Any(x => x == text))
                {
                    user.keyb = User.Main_Keyboard;
                    SendMessage(userID, wait_message,null,true);
                    return false;
                }
                SendMessage(userID, wrong_ans,null,true);
            }
            user.lastMsg.Item2 = true;
            WriteLine($"Get \"Ready\" from {userID}");
            ind.x = true;
            return true;
        }
    }
}
