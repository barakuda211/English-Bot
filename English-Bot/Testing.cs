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


        //отправляет сообщение юзеру
        static void SendMessage(long userID, string message, long[] msgIDs = null)
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
        static long WaitWordFromUser(long userID, string[] word, bool always)
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
                return (word.Any(x => x == user.lastMsg.Item1) ? 0 : user.lastMsg.Item3);
            }
            return 0;//заглушка
        }

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
            WaitWordFromUser(userID, agree.ToArray(), true);

            var rand = new Random();

            //если слов меньше, то так тому и быть
            List<long> lastULW = new List<long>();
            var w = users.GetUser(userID).unLearnedWords.ToArray();
            while (lastULW.Count < 3)
            {
                int i = rand.Next(w.Length);
                if (!lastULW.Contains(w[i]))
                    lastULW.Add(w[i]);
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
                    foreach (var def in word.mean_eng.def)
                        foreach (var tr in def.tr)
                            if (tr.syn != null)
                                wrds.AddRange(tr.syn.Select(x => x.text));
                }
                else
                {
                    wrds.Add(word.rus);
                    foreach (var def in word.mean_rus.def)
                        foreach (var tr in def.tr)
                            if (tr.syn != null)
                                wrds.AddRange(tr.syn.Select(x => x.text));
                }
                wrds.AddRange(wrds);
                msgIDs.Add(WaitWordFromUser(userID, wrds.ToArray(), false));
            }

            WriteLine("Слова пройдены");
            SendMessage(userID, $"Вы ответили на {msgIDs.FindAll(x => x == 0).Count()} из {lastULW.Count()}. ");

            //исправление ошибок юзера

            if (msgIDs.FindAll(x => x == 0).Count() < lastULW.Count())//если есть ошибки
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

            SendFullWordDescription(203654426, dictionary.GetEngWordIds("abandon").ElementAt(0));
            SendFullWordDescription(203654426, dictionary.GetEngWordIds("adore").ElementAt(0));
            SendFullWordDescription(203654426, dictionary.GetEngWordIds("accurate").ElementAt(0));
        }

        static void Testing_Start()
        {
            //122402184 - Dima
            //210036813 - Mike
            //223707460 - Anton
            long id = 203654426;

            //long id = 210036813;
            HashSet<long> hh = new HashSet<long>(dictionary.GetIds());
            users.GetUser(id).unLearnedWords = hh;

            /*dictionary.AddWord(new Word(1, "one", "van", "один", null, null, null, null, 1, null));
            dictionary.AddWord(new Word(2, "two", "too", "два", null, null, null, null, 1, null));
            dictionary.AddWord(new Word(1, "three", "tree", "три", null, null, null, null, 1, null));
            dictionary.AddWord(new Word(1, "four", "for", "четыре", null, null, null, null, 1, null));
            dictionary.AddWord(new Word(1, "five", "five", "пять", null, null, null, null, 1, null));
            dictionary.AddWord(new Word(1, "six", "siks", "шесть", null, null, null, null, 1, null));
            dictionary.AddWord(new Word(1, "seven", "seven", "семь", null, null, null, null, 1, null));*/
            //users.GetUser(id).learnedWords.Add(1);
            //users.GetUser(id).learnedWords.Add(2);

            Thread testingThread = new Thread(new ParameterizedThreadStart(Testing));
            testingThread.Start(id);
        }

    }
}
