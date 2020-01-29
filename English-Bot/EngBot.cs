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
using Project;
using Project_Word;
using System.Threading;


namespace English_Bot
{
    class EngBot
    {
        static Dictionary dictionary = new Dictionary();//подгрузку из файла нужно сделать
        static Users users = new Users();//подгрузку из файла нужно сделать(или из Resources)
        static VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl);
        static (long, string, bool) userMessage = (0, "", false);//переменная хранит последнее сообщение от всех пользователей(необходимо в user создать такое, чтобы хранилось последнее сообщение для каждого пользователя)
        static void Main(string[] args)
        {
            //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();
            //ILogger<VkBot> logger = loggerFactory.CreateLogger<VkBot>();

            //ExampleSettings settings = ExampleSettings.TryToLoad(logger);

            
            users.AddUser(new User(210036813, 1, new HashSet<string>(), new HashSet<long>(), new HashSet<long>())); 
             
            dictionary.AddWord(new Word(1, "one", "один", "", "", "", "", 1, null)) ;
            dictionary.AddWord(new Word(2, "two", "два", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(3, "three", "три", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(4, "four", "четыре", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(5, "five", "пять", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(6, "six", "шесть", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(7, "seven", "семь", "", "", "", "", 1, null));
            users.GetUser(1).learnedWords.Add(1);
            users.GetUser(1).learnedWords.Add(2);
            //users.GetUser(1).learnedWords.Add(3);
            //users.GetUser(1).learnedWords.Add(4);
            //users.GetUser(1).learnedWords.Add(5);
            //users.GetUser(1).learnedWords.Add(6);
            //users.GetUser(1).learnedWords.Add(7);

            long temp = 1;
            Thread testingThread = new Thread(new ParameterizedThreadStart(Testing));
            testingThread.Start(temp);

            bot.OnMessageReceived += NewMessageHandler;
            bot.Start();


            WriteLine("Bot has started!");
            ReadLine();
        }
        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {

            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId;
            var fromId = eventArgs.Message.FromId;
            var text = eventArgs.Message.Text;

            userMessage = (fromId.Value, text.ToLower(), false);

            //instanse.Logger.LogInformation($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
            WriteLine($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
            
            /*instanse.Api.Messages.Send(new MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                PeerId = eventArgs.Message.PeerId,
                Message =
                    $"{fromId.Value}, i have captured your message: '{text}'. its length is {text.Length}. number of spaces: {text.Count(x => x == ' ')}"
            });*/
        }
        //отправляет сообщение юзеру
        static void SendMessage(long userID, string message)
        {
            bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                UserId = userID,
                Message = message
            });
            WriteLine("слово отправлено");
        }
        //ждет ответа !определенного! ответа от юзера, 
        //always отвечает за время ожидания(false - 1 попытка, true - ждет, пока юзер не напишет нужное)
        static bool WaitWordFromUser(long userID, string word, bool always)
        {
            if (always)
            {
                while (userMessage != (userID, word, false)) Thread.Sleep(100);  //ожидание согласия
                WriteLine("готов получен");
                userMessage.Item3 = true;
            }
            if (!always)
            { 
                while (userMessage.Item1 != userID || userMessage.Item3 != false) Thread.Sleep(100);
                WriteLine(userMessage.Item1 + " " + userMessage.Item2 + " " + userMessage.Item3);
                userMessage.Item3 = true;
                return userMessage.Item2 == word;
            }
            return true;//заглушка
        }
        //тестирование пользователя по !6! последним изученным словам
        static void Testing(object IDobj)
        {
            long userID = users.GetUser((long)IDobj).userId; ;
            SendMessage(userID, "Вам будет предложен тест на знание английских слов. " +
                                "Не стоит подсматривать, от результатов теста зависит ваша дальнейшая программа обучения. " +
                                "Жду вашей команды: \"Готов\". ");
            WaitWordFromUser(userID, "готов", true);

            //если слов меньше, то так тому и быть
            var lastLW = users.GetUser((long)IDobj).learnedWords.TakeLast(6);

            var rand = new Random();

            //лист для проверки ответов
            List<bool> right = new List<bool>();

            foreach (long idx in lastLW)
            {
                var word = dictionary.GetWord(idx);
                int r = rand.Next(2);
                SendMessage(userID, (r == 0 ? word.eng : word.rus)); 
                right.Add(WaitWordFromUser(userID, (r == 1 ? word.eng : word.rus), false));
            }

            WriteLine("Слова пройдены");
            SendMessage(userID, $"Вы ответили на {right.FindAll(x => x).Count()} из {lastLW.Count()}. Good job!(no)");

            //исправление ошибок юзера

            if (right.FindAll(x => x).Count() < lastLW.Count())
            {
                string fixAnswer = "Вы ошиблись в следующем:";

                foreach (var pnt in right.Zip(lastLW, (x, y) => new { A = x, B = y }))
                {
                    if (!pnt.A)
                    {
                        var temp = dictionary.GetWord(pnt.B);
                        fixAnswer += $"\n{temp.eng} - {temp.rus}";
                    }
                }
                SendMessage(userID, fixAnswer);
            }
        }
    }
}
