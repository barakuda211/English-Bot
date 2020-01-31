using System;
using System.Collections.Generic;
using VkBotFramework;
using VkBotFramework.Models;
using System.Linq;
using static System.Console;
using System.Threading;

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
        static long WaitWordFromUser(long userID, string word, bool always)
        {
            var user = users.GetUser(userID);
            if (always)
            {
                while (user.lastMsg.Item1 != word || user.lastMsg.Item2 != false) Thread.Sleep(100);  //ожидание согласия
                WriteLine("готов получен");
                user.lastMsg.Item2 = true;
            }
            if (!always)
            {
                while (user.lastMsg.Item2 != false) Thread.Sleep(100);
                WriteLine("слово получено");
                WriteLine(user.lastMsg.Item3);
                user.lastMsg.Item2 = true;
                return (user.lastMsg.Item1 == word ? 0 : user.lastMsg.Item3);
            }
            return 0;//заглушка
        }

        //тестирование пользователя по !6! последним изученным словам
        static void Testing(object IDobj)
        {
            long userID = (long)IDobj;
            SendMessage(userID, "Вам будет предложен тест на знание английских слов. " +
                                "Не стоит подсматривать, от результатов теста зависит ваша дальнейшая программа обучения. " +
                                "Жду вашей команды: \"Готов\". ");
            WaitWordFromUser(userID, "готов", true);

            //если слов меньше, то так тому и быть
            var lastLW = users.GetUser(userID).learnedWords.TakeLast(6);

            var rand = new Random();

            //лист для проверки ответов
            List<long> msgIDs = new List<long>();

            foreach (long idx in lastLW)//присылает и ждет ответ на последние изученные слова
            {
                var word = dictionary.GetWord(idx);
                int r = rand.Next(2);
                SendMessage(userID, (r == 0 ? word.eng : word.rus));
                msgIDs.Add(WaitWordFromUser(userID, (r == 1 ? word.eng : word.rus), false));
            }

            WriteLine("Слова пройдены");
            SendMessage(userID, $"Вы ответили на {msgIDs.FindAll(x => x == 0).Count()} из {lastLW.Count()}. ");

            //исправление ошибок юзера

            if (msgIDs.FindAll(x => x == 0).Count() < lastLW.Count())//если есть ошибки
            {
                SendMessage(userID, "Вы ошиблись в следующем:");

                long[] aError = new long[1];
                foreach (var pnt in msgIDs.Zip(lastLW, (x, y) => new { A = x, B = y }))
                {
                    if (pnt.A > 0)//идет по ошибкам
                    {
                        var temp = dictionary.GetWord(pnt.B);
                        //aError[0] = pnt.A;//массив с 1 пересланным сообщением, где юзер сделал ошибку
                        SendMessage(userID, $"\n{temp.eng} - {temp.rus}"/*, aError*/);
                    }
                }
            }
        }

        static void Testing_Start()
        {
            //122402184 - Dima
            //210036813 - Mike
            //223707460 - Anton
            long id = 210036813;
            users.AddUser(new User(id, 1, new HashSet<string>(), new HashSet<long>(), new HashSet<long>()));

            dictionary.AddWord(new Word(1, "one", "один", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(2, "two", "два", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(3, "three", "три", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(4, "four", "четыре", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(5, "five", "пять", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(6, "six", "шесть", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(7, "seven", "семь", "", "", "", "", 1, null));
            users.GetUser(id).learnedWords.Add(1);
            users.GetUser(id).learnedWords.Add(2);

            Thread testingThread = new Thread(new ParameterizedThreadStart(Testing));
            testingThread.Start(id);
        }

    }
}
