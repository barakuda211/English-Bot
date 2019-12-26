using System;
using System.Collections.Generic;
using System.Text;
using VkBotFramework;
using English_Bot.Properties;
using VkNet.Model;
using VkApi;

namespace English_Bot
{
    partial class EngBot
    {

        static string Registration(Message msg)
        {
            var id = msg.FromId;
            if (!UsersDictionary.HasUser(msg.FromId))
            {
                VkUser vk = VkRequests.VkRequestUser(id);  //получаем юзера из вк
                User us = new User(vk, wordsDictionary);
                UsersDictionary.AddUser(us);  //создаём нового юзера
            }
            User user = UsersDictionary[id];
            switch (user.regId++)
            {
                case 0:
                    Console.WriteLine("Registered: "+ user.name+" "+user.userId);
                    return "Приветствую, " + user.name+"!\n" +
                        "Вам будет предложен тест на знание английских слов.\n" +
                        "Не стоит подсматривать, от результатов теста зависит ваша дальнейшая программа обучения.\n" +
                        "Жду вашей команды: \"Готов\".";
                    break;
                case 1:
                    if(msg.Text.ToLower() == "готов")
                    return "redId =" + user.regId;
                    break;
                default: return "end!"; 
                    break;
            }
        }
    }
}
