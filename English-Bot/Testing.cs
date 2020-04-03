using System;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
using System.Linq;
using static System.Console;
using System.Threading;
using Project_Word;
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
        public static void SendMessage(long userID, string message, long[] msgIDs = null)
        {
            bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                RandomId = Environment.TickCount64,
                UserId = userID,
                Message = message,
                ForwardMessages = msgIDs
            });
            WriteLine("слово отправлено");
        }

        //ждет ответа !определенного! ответа от юзера, 
        //always отвечает за время ожидания(false - 1 попытка, true - ждет, пока юзер не напишет нужное)
        static long WaitWordFromUser(long userID, string[] word, bool always, long word_id)
        {
            var user = users.GetUser(userID);
            if (always)
            {
                while (word.All(x => x.ToLower() != user.lastMsg.Item1.ToLower()) || user.lastMsg.Item2) Thread.Sleep(100);  //ожидание согласия
                WriteLine("готов получен");
                user.lastMsg.Item2 = true;
            }
            if (!always)
            {
                while (user.lastMsg.Item2 != false) Thread.Sleep(100);
                WriteLine("слово получено");
                WriteLine(user.lastMsg.Item3);
                user.lastMsg.Item2 = true;
                return (word.Any(x => x == user.lastMsg.Item1) ? -word_id : user.lastMsg.Item3);
            }
            return 0;//заглушка
        }

        static int TEST_Words = 5; 

        //тестирование пользователя по !6! последним изученным словам
        static void Testing(object IDobj)
        {
            long userID = (long)IDobj;
            //Console.WriteLine("Number of words = " + users.GetUser(userID).unLearnedWords.Count);
            SendMessage(userID, "Вам будет предложен тест на знание английских слов. " +
                                "Не стоит подсматривать, от результатов теста зависит ваша дальнейшая программа обучения. " +
                                "Жду вашей команды: \"Готов\". ");
            List<string> agree = new List<string>();
            agree.Add("Готов");
            agree.Add("Да");
            agree.Add("точно");
            WaitWordFromUser(userID, agree.ToArray(), true, -1);

            var rand = new Random();
            /*
            //если слов меньше, то так тому и быть
            List<long> lastULW = new List<long>();
            var w = users.GetUser(userID).unLearnedWords.ToArray(); 
            while (lastULW.Count < 5)
            {
                int i = rand.Next(w.Length);    
                if (!lastULW.Contains(w[i]))
                    lastULW.Add(w[i]);
            } 
            */

            HashSet<long> lastULW = new HashSet<long>();
            var w = users.GetUser(userID).unLearnedWords.ToArray();
            int add_words = 0;
            if (w.Count() < 5)
                add_words = 5 - w.Count();

            
            for (int i = 0; i < w.Count(); i++)
            {
                int j = rand.Next(0,w.Count());      
                if (!lastULW.Contains(w[j]))
                    lastULW.Add(w[j]);
            }

            //дополнение списка слов
            for (int i = 0; i < add_words; i++)             //TODO: заменить на слова нужного уровня, если возможно
            {
                int j = rand.Next(dictionary.Count());
                if (!lastULW.Contains(dictionary.GetKeys().ElementAt(j)))
                    lastULW.Add(dictionary.GetKeys().ElementAt(j));
            }

            //лист для проверки ответов
            List<long> msgIDs = new List<long>();

            foreach (long idx in lastULW)//присылает и ждет ответ на последние изученные слова
            {
                var word = dictionary.GetWord(idx);
                int r = rand.Next(2);
                SendMessage(userID, (r == 0 ? word.eng : word.rus));
                List<string> wrds = new List<string>();
                if (r == 1)
                {
                    wrds.Add(word.eng);
                    if (word.mean_eng != null)
                        foreach (var def in word.mean_eng.def)
                            foreach (var tr in def.tr)
                                if (tr.syn != null)
                                    wrds.AddRange(tr.syn.Select(x => x.text));
                }
                else
                {
                    if (word.rus != null)
                        wrds.Add(word.rus);
                    if (word.mean_rus != null)
                        foreach (var def in word.mean_rus.def)
                            foreach (var tr in def.tr)
                                if (tr.syn != null)
                                    wrds.AddRange(tr.syn.Select(x => x.text));
                }
                // wrds.AddRange(wrds);
                msgIDs.Add(WaitWordFromUser(userID, wrds.ToArray(), false, idx));
            }

            if (msgIDs.Any(x => x < 0) && users[userID].learnedWords == null)
                users[userID].learnedWords = new HashSet<long>();

            // Добавляем верные ответы в изученные слова и  убираем из невыученных
            foreach (var id in msgIDs.FindAll(x => x < 0))
            {
                try
                {
                    users[userID].learnedWords.Add(-id);
                    users[userID].unLearnedWords.Remove(-id);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Something wrong in removing unlearned or adding learned words");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            WriteLine("Слова пройдены");
            SendMessage(userID, $"Вы ответили на {msgIDs.FindAll(x => x < 0).Count()} из {lastULW.Count()}. ");

            //исправление ошибок юзера

            if (msgIDs.FindAll(x => x < 0).Count() < lastULW.Count())//если есть ошибки
            {
                SendMessage(userID, "Вы ошиблись в следующем:");

                long[] aError = new long[1];
                foreach (var pnt in msgIDs.Zip(lastULW, (x, y) => new { A = x, B = y }))
                {
                    if (pnt.A > 0)//идет по ошибкам
                    {
                        var temp = dictionary.GetWord(pnt.B);
                        //aError[0] = pnt.A;//массив с 1 пересланным сообщением, где юзер сделал ошибку
                        SendMessage(userID, $"\n{temp.eng} - {temp.rus}"/*, aError*/);
                    }
                }
            }

            New:
            List<long> words_level = dictionary.GetKeysByLevel(users[userID].userLevel).Where(x => !users[userID].learnedWords.Contains(x)).ToList();
            if(Math.Abs(words_level.Count - users[userID].unLearnedWords.Count) < Users.UNLearned)
            {
                if (users[userID].userLevel == 5)
                    users[userID].userLevel = -1;
                else if (users[userID].userLevel == -1)
                    goto Fin;
                goto New;
            }
            while (users[userID].unLearnedWords.Count < Users.UNLearned)
            {
                int value = rand.Next(words_level.Count);
                users[userID].unLearnedWords.Add(words_level.ElementAt(value));
            }
        Fin:
            users[userID].on_Test = false;

            users.Save();
        }

        static void Testing_Start(long id)
        {
            users[id].on_Test = true;
            Thread testingThread = new Thread(new ParameterizedThreadStart(Testing));
            testingThread.Start(id);
        }

    }
}
